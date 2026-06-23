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
                .Include(p => p.Usuario)
                .Include(p => p.Aula)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Progress
                .Include(p => p.Usuario)
                .Include(p => p.Aula)
                .FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // Usado para salvar progresso (ex.: quando assistir vídeo)
        [Authorize]
        [HttpPost]
        public IActionResult Criar(Progress dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            var aulaExists = _context.Aulas.Any(a => a.Id == dto.AulaId);
            if (!userExists || !aulaExists) return BadRequest(new { sucesso = false, message = "Usuário ou Aula inexistente" });

            var novo = new Progress
            {
                UsuarioId = dto.UsuarioId,
                AulaId = dto.AulaId,
                Percentual = dto.Percentual,
                TempoSegundos = dto.TempoSegundos,
                AtualizadoEm = DateTime.UtcNow
            };

            _context.Progress.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Progress dto)
        {
            var item = _context.Progress.FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.Percentual = dto.Percentual;
            item.TempoSegundos = dto.TempoSegundos;
            item.AtualizadoEm = DateTime.UtcNow;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Progresso atualizado", data = item });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Progress.FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();

            _context.Progress.Remove(item);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Progresso apagado" });
        }
    }
}
