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
        private readonly ILogger<MidiasController> _logger;

        public MidiasController(AppDbContext context, ILogger<MidiasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var midias = _context.Midias
                    .Include(m => m.aula)
                    .ThenInclude(a => a.modulo)
                    .ThenInclude(mod => mod.treinamento)
                    .ToList();

                return Ok(midias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar mídias");
                return StatusCode(500, new { mensagem = "Não foi possível listar as mídias" });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try
            {
                var midia = _context.Midias
                    .Include(m => m.aula)
                    .ThenInclude(a => a.modulo)
                    .ThenInclude(mod => mod.treinamento)
                    .FirstOrDefault(m => m.id == id);

                if (midia == null) return NotFound();

                return Ok(midia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mídia {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível obter a mídia" });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Midias dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var aulaExiste = _context.Aulas.Any(a => a.id == dto.aula_id);
                if (!aulaExiste)
                    return BadRequest(new { sucesso = false, message = "Aula inexistente" });

                var novo = new Midias
                {
                    nome = dto.nome,
                    url = dto.url,
                    tipo = dto.tipo,
                    aula_id = dto.aula_id,
                    criado = DateTime.UtcNow
                };

                _context.Midias.Add(novo);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar mídia");
                return StatusCode(500, new { mensagem = "Não foi possível criar a mídia" });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Midias dto)
        {
            try
            {
                var midia = _context.Midias.FirstOrDefault(m => m.id == id);
                if (midia == null) return NotFound();

                if (dto == null) return BadRequest();

                var aulaExiste = _context.Aulas.Any(a => a.id == dto.aula_id);
                if (!aulaExiste)
                    return BadRequest(new { sucesso = false, message = "Aula inexistente" });

                midia.nome = dto.nome;
                midia.url = dto.url;
                midia.tipo = dto.tipo;
                midia.aula_id = dto.aula_id;

                _context.SaveChanges();

                return Ok(new { sucesso = true, message = "Mídia atualizada com sucesso", data = midia });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar mídia {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível editar a mídia" });
            }
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var midia = _context.Midias.FirstOrDefault(m => m.id == id);
                if (midia == null) return NotFound();

                _context.Midias.Remove(midia);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar mídia {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível deletar a mídia" });
            }
        }
    }
}
