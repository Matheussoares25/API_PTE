using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using System.Linq;
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
                .Include(a => a.Modulo)
                .ThenInclude(m => m.Treinamento)
                .ToList();

            return Ok(aulas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var aula = _context.Aulas
                .Include(a => a.Modulo)
                .ThenInclude(m => m.Treinamento)
                .FirstOrDefault(a => a.Id == id);

            if (aula == null) return NotFound();

            return Ok(aula);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Aulas dto)
        {
            if (dto == null) return BadRequest();

            var moduloExiste = _context.Modulos.Any(m => m.Id == dto.ModuloId);
            if (!moduloExiste)
            {
                return BadRequest(new { sucesso = false, message = "Módulo inexistente" });
            }

            var novo = new Aulas
            {
                Nome = dto.Nome,
                Conteudo = dto.Conteudo,
                ModuloId = dto.ModuloId,
                Criado = DateTime.UtcNow
            };

            _context.Aulas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Aulas dto)
        {
            var aula = _context.Aulas.FirstOrDefault(a => a.Id == id);
            if (aula == null) return NotFound();

            if (dto == null) return BadRequest();

            var moduloExiste = _context.Modulos.Any(m => m.Id == dto.ModuloId);
            if (!moduloExiste)
            {
                return BadRequest(new { sucesso = false, message = "Módulo inexistente" });
            }

            aula.Nome = dto.Nome;
            aula.Conteudo = dto.Conteudo;
            aula.ModuloId = dto.ModuloId;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Aula atualizada com sucesso", data = aula });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aula = _context.Aulas.FirstOrDefault(a => a.Id == id);
            if (aula == null) return NotFound();

            _context.Aulas.Remove(aula);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Aula apagada" });
        }
    }
}
