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
        private readonly ILogger<AulasController> _logger;

        public AulasController(AppDbContext context, ILogger<AulasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todas as aulas de um módulo específico.
        /// </summary>
        [Authorize(Roles = "2,3")]
        [HttpGet("modulo/{moduloId}")]
        public IActionResult ObterPorModulo(int moduloId)
        {
            try
            {
                var aulas = _context.Aulas
                    .Where(a => a.modulo_id == moduloId)
                    .Include(a => a.modulo)
                    .ThenInclude(m => m.treinamento)
                    .ToList();

                return Ok(aulas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar aulas do módulo {ModuloId}", moduloId);
                return StatusCode(500, new { mensagem = "Não foi possível listar as aulas do módulo" });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var aulas = _context.Aulas
                    .Include(a => a.modulo)
                    .ThenInclude(m => m.treinamento)
                    .ToList();

                return Ok(aulas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar aulas");
                return StatusCode(500, new { mensagem = "Não foi possível listar as aulas" });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try
            {
                var aula = _context.Aulas
                    .Include(a => a.modulo)
                    .ThenInclude(m => m.treinamento)
                    .FirstOrDefault(a => a.id == id);

                if (aula == null) return NotFound();

                return Ok(aula);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter aula {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível obter a aula" });
            }
        }

        //[Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Aulas dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var moduloExiste = _context.Modulos.Any(m => m.id == dto.modulo_id);
                if (!moduloExiste)
                    return BadRequest(new { sucesso = false, message = "Módulo inexistente" });

                var novo = new Aulas
                {
                    nome = dto.nome,
                    conteudo = dto.conteudo,
                    modulo_id = dto.modulo_id,
                    midia_url = dto.midia_url,
                    criado = DateTime.UtcNow
                };

                _context.Aulas.Add(novo);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar aula");
                return StatusCode(500, new { message = "Não foi possível criar a aula" });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Aulas dto)
        {
            try
            {
                var aula = _context.Aulas.FirstOrDefault(a => a.id == id);
                if (aula == null) return NotFound();

                if (dto == null) return BadRequest();

                var moduloExiste = _context.Modulos.Any(m => m.id == dto.modulo_id);
                if (!moduloExiste)
                    return BadRequest(new { sucesso = false, message = "Módulo inexistente" });

                // midia_url é apenas uma string; não há validação contra tabela externa

                aula.nome = dto.nome;
                aula.conteudo = dto.conteudo;
                aula.modulo_id = dto.modulo_id;
                aula.midia_url = dto.midia_url;

                _context.SaveChanges();

                return Ok(new { sucesso = true, message = "Aula atualizada com sucesso", data = aula });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar aula {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível editar a aula" });
            }
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var aula = _context.Aulas.FirstOrDefault(a => a.id == id);
                if (aula == null) return NotFound();

                _context.Aulas.Remove(aula);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar aula {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível deletar a aula" });
            }
        }
    }
}