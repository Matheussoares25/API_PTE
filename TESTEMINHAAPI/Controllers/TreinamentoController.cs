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
        [Authorize(Roles = "2,3")]
        [HttpGet]
        public IActionResult BuscaTreinamentos()
        {
                          
           var treinamentos = _context.Treinamentos.ToList();
           return Ok(treinamentos);
            

        }

        /// <summary>
        /// Retorna todos os cursos com seus módulos e as aulas de cada módulo.
        /// </summary>
        [Authorize(Roles = "2,3")]
        [HttpGet("completo")]
        public IActionResult CursosComModulosEAulas()
        {
            try
            {
                var cursos = _context.Treinamentos
                    .Select(t => new
                    {
                        id = t.id,
                        nome = t.nome,
                        criado = t.criado,
                        modulos = _context.Modulos
                            .Where(m => m.treinamento_id == t.id)
                            .Select(m => new
                            {
                                id = m.id,
                                nome = m.nome,
                                aulas = _context.Aulas
                                    .Where(a => a.modulo_id == m.id)
                                    .Select(a => new { a.id, a.nome, a.conteudo, a.criado })
                                    .ToList()
                            })
                            .ToList()
                    })
                    .ToList();

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { sucesso = false, message = "Ocorreu um erro ao listar os cursos com módulos e aulas.", erro = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var treino = _context.Treinamentos.FirstOrDefault(t => t.id == id);
            if (treino == null) return NotFound();
            return Ok(treino);
        }

        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Treinamentos treinamento)
        {
            treinamento.criado = DateTime.Now;
            _context.Treinamentos.Add(treinamento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarPorId), new { id = treinamento.id }, treinamento);
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
