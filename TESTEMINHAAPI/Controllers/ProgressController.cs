using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProgressController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.Progress
                .Include(p => p.usuario)
                .Include(p => p.aula)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Progress
                .Include(p => p.usuario)
                .Include(p => p.aula)
                .FirstOrDefault(p => p.id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // Usado para salvar progresso (ex.: quando assistir vídeo)
        [Authorize]
        [HttpPost]
        public IActionResult Criar(Progress dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
            var aulaExists = _context.Aulas.Any(a => a.id == dto.aula_id);
            if (!userExists || !aulaExists) return BadRequest(new { sucesso = false, message = "Usuário ou Aula inexistente" });

            var novo = new Progress
            {
                usuario_id = dto.usuario_id,
                aula_id = dto.aula_id,
                percentual = dto.percentual,
                tempo_segundos = dto.tempo_segundos,
                atualizado_em = DateTime.UtcNow
            };

            _context.Progress.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Progress dto)
        {
            var item = _context.Progress.FirstOrDefault(p => p.id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.percentual = dto.percentual;
            item.tempo_segundos = dto.tempo_segundos;
            item.atualizado_em = DateTime.UtcNow;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Progresso atualizado", data = item });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Progress.FirstOrDefault(p => p.id == id);
            if (item == null) return NotFound();

            _context.Progress.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
