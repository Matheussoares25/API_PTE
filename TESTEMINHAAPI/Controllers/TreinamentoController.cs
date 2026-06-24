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
                          
           var treinamentos = _context.Treinamentos.ToList();
           return Ok(treinamentos);
            

            return BadRequest();

        }
        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Treinamentos treinamento)
        {
            treinamento.criado = DateTime.Now;
            _context.Treinamentos.Add(treinamento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscaTreinamentos), new { id = treinamento.id }, treinamento);
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Treinamentos dto)
        {
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

            var treino = _context.Treinamentos.FirstOrDefault(t => t.id == id);
            if (treino == null) return NotFound();

            _context.Treinamentos.Remove(treino);
            _context.SaveChanges();
            return NoContent();
        }
}
}
