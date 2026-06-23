using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using System;
using TESTEMINHAAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestoesController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var questoes = _context.Questoes
                .Include(q => q.Aula)
                .ThenInclude(a => a.Modulo)
                .ThenInclude(m => m.Treinamento)
                .ToList();

            return Ok(questoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var questao = _context.Questoes
                .Include(q => q.Aula)
                .ThenInclude(a => a.Modulo)
                .ThenInclude(m => m.Treinamento)
                .FirstOrDefault(q => q.Id == id);

            if (questao == null) return NotFound();

            return Ok(questao);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Questoes dto)
        {
            if (dto == null) return BadRequest();

            var aulaExiste = _context.Aulas.Any(a => a.Id == dto.AulaId);
            if (!aulaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Aula inexistente" });
            }

            var novo = new Questoes
            {
                Texto = dto.Texto,
                AulaId = dto.AulaId,
                Criado = DateTime.UtcNow
            };

            _context.Questoes.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Questoes dto)
        {
            var questao = _context.Questoes.FirstOrDefault(q => q.Id == id);
            if (questao == null) return NotFound();

            if (dto == null) return BadRequest();

            var aulaExiste = _context.Aulas.Any(a => a.Id == dto.AulaId);
            if (!aulaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Aula inexistente" });
            }

            questao.Texto = dto.Texto;
            questao.AulaId = dto.AulaId;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Questão atualizada com sucesso", data = questao });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var questao = _context.Questoes.FirstOrDefault(q => q.Id == id);
            if (questao == null) return NotFound();

            _context.Questoes.Remove(questao);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Questão apagada" });
        }
    }
}
