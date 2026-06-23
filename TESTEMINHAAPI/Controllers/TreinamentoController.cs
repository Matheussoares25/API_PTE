using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.BancoDeDados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;


namespace TESTEMINHAAPI.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class TreinamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TreinamentoController(AppDbContext context) { 
            
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult BuscaTreinamentos()
        {
            var tipouser = User.FindFirst("Tipo")?.Value;

            if (tipouser == "2")
            {
                var treinamentos = _context.Treinamentos.ToList();
                return Ok(treinamentos);
            }

            return BadRequest();

        }
        [Authorize]
        [HttpPost]
        public IActionResult Criar(Treinamentos treinamento)
        {
            var tipouser = User.FindFirst("Tipo")?.Value;
            if (tipouser != "2") return BadRequest();

            treinamento.criado = DateTime.Now;
            _context.Treinamentos.Add(treinamento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscaTreinamentos), new { id = treinamento.id }, treinamento);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Treinamentos dto)
        {
            var tipouser = User.FindFirst("Tipo")?.Value;
            if (tipouser != "2") return BadRequest();

            var treino = _context.Treinamentos.FirstOrDefault(t => t.id == id);
            if (treino == null) return NotFound();

            treino.nome = dto.nome;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Treinamento atualizado com sucesso", data = treino });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tipouser = User.FindFirst("Tipo")?.Value;
            if (tipouser != "2") return BadRequest();

            var treino = _context.Treinamentos.FirstOrDefault(t => t.id == id);
            if (treino == null) return NotFound();

            _context.Treinamentos.Remove(treino);
            _context.SaveChanges();
            return NoContent();
        }
}
}
