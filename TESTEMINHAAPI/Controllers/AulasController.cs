using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AulasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var aulas = _context.Aulas
                .Include(a => a.modulo)
                .ThenInclude(m => m.treinamento)
                .ToList();

            return Ok(aulas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var aula = _context.Aulas
                .Include(a => a.modulo)
                .ThenInclude(m => m.treinamento)
                .FirstOrDefault(a => a.id == id);

            if (aula == null) return NotFound();

            return Ok(aula);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Aulas dto)
        {
            if (dto == null) return BadRequest();

            var moduloExiste = _context.Modulos.Any(m => m.id == dto.modulo_id);
            if (!moduloExiste)
            {
                return BadRequest(new { sucesso = false, message = "Módulo inexistente" });
            }

            var novo = new Aulas
            {
                nome = dto.nome,
                conteudo = dto.conteudo,
                modulo_id = dto.modulo_id,
                criado = DateTime.UtcNow
            };

            _context.Aulas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Aulas dto)
        {
            var aula = _context.Aulas.FirstOrDefault(a => a.id == id);
            if (aula == null) return NotFound();

            if (dto == null) return BadRequest();

            var moduloExiste = _context.Modulos.Any(m => m.id == dto.modulo_id);
            if (!moduloExiste)
            {
                return BadRequest(new { sucesso = false, message = "Módulo inexistente" });
            }

            aula.nome = dto.nome;
            aula.conteudo = dto.conteudo;
            aula.modulo_id = dto.modulo_id;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Aula atualizada com sucesso", data = aula });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aula = _context.Aulas.FirstOrDefault(a => a.id == id);
            if (aula == null) return NotFound();

            _context.Aulas.Remove(aula);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
