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
    public class AlternativasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlternativasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var alternativas = _context.Alternativas
                .Include(a => a.questao)
                .ThenInclude(q => q.aula)
                .ToList();

            return Ok(alternativas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var alternativa = _context.Alternativas
                .Include(a => a.questao)
                .ThenInclude(q => q.aula)
                .FirstOrDefault(a => a.id == id);

            if (alternativa == null) return NotFound();

            return Ok(alternativa);
        }

        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Alternativas dto)
        {
            if (dto == null) return BadRequest();

            var questaoExiste = _context.Questoes.Any(q => q.id == dto.questao_id);
            if (!questaoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Questão inexistente" });
            }

            var novo = new Alternativas
            {
                texto = dto.texto,
                url = dto.url,
                correta = dto.correta,
                ordem = dto.ordem,
                questao_id = dto.questao_id,
                criado = DateTime.UtcNow
            };

            _context.Alternativas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Alternativas dto)
        {
            var alt = _context.Alternativas.FirstOrDefault(a => a.id == id);
            if (alt == null) return NotFound();

            if (dto == null) return BadRequest();

            var questaoExiste = _context.Questoes.Any(q => q.id == dto.questao_id);
            if (!questaoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Questão inexistente" });
            }

            alt.texto = dto.texto;
            alt.url = dto.url;
            alt.correta = dto.correta;
            alt.ordem = dto.ordem;
            alt.questao_id = dto.questao_id;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Alternativa atualizada com sucesso", data = alt });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alt = _context.Alternativas.FirstOrDefault(a => a.id == id);
            if (alt == null) return NotFound();

            _context.Alternativas.Remove(alt);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
