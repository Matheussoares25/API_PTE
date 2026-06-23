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
                .Include(m => m.Aula)
                .ThenInclude(a => a.Modulo)
                .ThenInclude(mod => mod.Treinamento)
                .ToList();

            return Ok(midias);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var midia = _context.Midias
                .Include(m => m.Aula)
                .ThenInclude(a => a.Modulo)
                .ThenInclude(mod => mod.Treinamento)
                .FirstOrDefault(m => m.Id == id);

            if (midia == null) return NotFound();

            return Ok(midia);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Midias dto)
        {
            if (dto == null) return BadRequest();

            var aulaExiste = _context.Aulas.Any(a => a.Id == dto.AulaId);
            if (!aulaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Aula inexistente" });
            }

            var novo = new Midias
            {
                Nome = dto.Nome,
                Url = dto.Url,
                Tipo = dto.Tipo,
                AulaId = dto.AulaId,
                Criado = DateTime.UtcNow
            };

            _context.Midias.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Midias dto)
        {
            var midia = _context.Midias.FirstOrDefault(m => m.Id == id);
            if (midia == null) return NotFound();

            if (dto == null) return BadRequest();

            var aulaExiste = _context.Aulas.Any(a => a.Id == dto.AulaId);
            if (!aulaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Aula inexistente" });
            }

            midia.Nome = dto.Nome;
            midia.Url = dto.Url;
            midia.Tipo = dto.Tipo;
            midia.AulaId = dto.AulaId;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Mídia atualizada com sucesso", data = midia });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var midia = _context.Midias.FirstOrDefault(m => m.Id == id);
            if (midia == null) return NotFound();

            _context.Midias.Remove(midia);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Mídia apagada" });
        }
    }
}
