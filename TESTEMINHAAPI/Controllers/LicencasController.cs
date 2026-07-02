using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using System.Linq;
using System;
using System.Security.Cryptography;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicencasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LicencasController> _logger;

        public LicencasController(AppDbContext context, ILogger<LicencasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var list = _context.Licencas.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar licenças");
                return StatusCode(500, new { mensagem = "Não foi possível listar as licenças" });
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult PorUsuario(int usuarioId)
        {
            try
            {
                var list = _context.Licencas
                    .Where(l => l.usuario_id == usuarioId)
                    .ToList();

                if (list == null || list.Count == 0) return NotFound(new { message = "Usuário não encontrado." });

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar licenças do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, new { mensagem = "Não foi possível listar as licenças do usuário" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try
            {
                var item = _context.Licencas.FirstOrDefault(l => l.id == id);
                if (item == null) return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter licença {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível obter a licença" });
            }
        }

        //[Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar([FromBody] Licencas dto)
        {
            try
            {
                if (dto == null) return BadRequest(new { message = "Dados inválidos." });

                string token;
                do
                {
                    token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
                } while (_context.Licencas.Any(l => l.token == token));

                var novo = new Licencas
                {
                    usuario_id = dto.usuario_id,
                    token = token,
                    criado_em = dto.criado_em == default ? DateTime.UtcNow : dto.criado_em,
                    validade_em = dto.validade_em == default ? DateTime.UtcNow.AddYears(1) : dto.validade_em,
                    ativo = dto.ativo,
                    preco = dto.preco
                };

                _context.Licencas.Add(novo);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar licença");
                return StatusCode(500, new { mensagem = "Não foi possível criar a licença" });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Licencas dto)
        {
            try
            {
                var item = _context.Licencas.FirstOrDefault(l => l.id == id);
                if (item == null) return NotFound();
                if (dto == null) return BadRequest(new { message = "Dados inválidos." });

                item.usuario_id = dto.usuario_id;
                item.validade_em = dto.validade_em;
                item.ativo = dto.ativo;
                item.preco = dto.preco;

                _context.SaveChanges();

                return Ok(new { sucesso = true, message = "Licença atualizada com sucesso", data = item });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar licença {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível editar a licença" });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _context.Licencas.FirstOrDefault(l => l.id == id);
                if (item == null) return NotFound();

                _context.Licencas.Remove(item);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar licença {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível deletar a licença" });
            }
        }
    }
}
