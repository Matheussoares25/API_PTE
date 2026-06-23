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
                .Include(a => a.Questao)
                .ThenInclude(q => q.Aula)
                .ToList();

            return Ok(alternativas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var alternativa = _context.Alternativas
                .Include(a => a.Questao)
                .ThenInclude(q => q.Aula)
                .FirstOrDefault(a => a.Id == id);

            if (alternativa == null) return NotFound();

            return Ok(alternativa);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Alternativas dto)
        {
            if (dto == null) return BadRequest();

            var questaoExiste = _context.Questoes.Any(q => q.Id == dto.QuestaoId);
            if (!questaoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Questão inexistente" });
            }

            var novo = new Alternativas
            {
                Texto = dto.Texto,
                Url = dto.Url,
                Correta = dto.Correta,
                Ordem = dto.Ordem,
                QuestaoId = dto.QuestaoId,
                Criado = DateTime.UtcNow
            };

            _context.Alternativas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Alternativas dto)
        {
            var alt = _context.Alternativas.FirstOrDefault(a => a.Id == id);
            if (alt == null) return NotFound();

            if (dto == null) return BadRequest();

            var questaoExiste = _context.Questoes.Any(q => q.Id == dto.QuestaoId);
            if (!questaoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Questão inexistente" });
            }

            alt.Texto = dto.Texto;
            alt.Url = dto.Url;
            alt.Correta = dto.Correta;
            alt.Ordem = dto.Ordem;
            alt.QuestaoId = dto.QuestaoId;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Alternativa atualizada com sucesso", data = alt });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alt = _context.Alternativas.FirstOrDefault(a => a.Id == id);
            if (alt == null) return NotFound();

            _context.Alternativas.Remove(alt);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Alternativa apagada" });
        }
    }
}
