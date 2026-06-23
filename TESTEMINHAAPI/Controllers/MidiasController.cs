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
    public class MidiasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MidiasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var midias = _context.Midias
                .Include(m => m.aula)
                .ThenInclude(a => a.modulo)
                .ThenInclude(mod => mod.treinamento)
                .ToList();

            return Ok(midias);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var midia = _context.Midias
                .Include(m => m.aula)
                .ThenInclude(a => a.modulo)
                .ThenInclude(mod => mod.treinamento)
                .FirstOrDefault(m => m.id == id);

            if (midia == null) return NotFound();

            return Ok(midia);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Midias dto)
        {
            if (dto == null) return BadRequest();

            var aulaExiste = _context.Aulas.Any(a => a.id == dto.aula_id);
            if (!aulaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Aula inexistente" });
            }

            var novo = new Midias
            {
                nome = dto.nome,
                url = dto.url,
                tipo = dto.tipo,
                aula_id = dto.aula_id,
                criado = DateTime.UtcNow
            };

            _context.Midias.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Midias dto)
        {
            var midia = _context.Midias.FirstOrDefault(m => m.id == id);
            if (midia == null) return NotFound();

            if (dto == null) return BadRequest();

            var aulaExiste = _context.Aulas.Any(a => a.id == dto.aula_id);
            if (!aulaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Aula inexistente" });
            }

            midia.nome = dto.nome;
            midia.url = dto.url;
            midia.tipo = dto.tipo;
            midia.aula_id = dto.aula_id;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Mídia atualizada com sucesso", data = midia });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var midia = _context.Midias.FirstOrDefault(m => m.id == id);
            if (midia == null) return NotFound();

            _context.Midias.Remove(midia);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
