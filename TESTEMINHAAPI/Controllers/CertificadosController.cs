using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.DTOS;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificadosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CertificadosController> _logger;

        public CertificadosController(AppDbContext context, ILogger<CertificadosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var list = _context.Certificados
                    .Include(c => c.usuario)
                    .Include(c => c.treinamento)
                    .ToList();

                var resultado = list.Select(c => new
                {
                    id = c.id,
                    usuario = new UsuarioDTO
                    {
                        id = c.usuario.id,
                        email = c.usuario.email,
                        ativo = c.usuario.ativo,
                        nome = c.usuario.nome
                    },
                    treinamento = c.treinamento,
                    codigo = c.codigo,
                    emitido_em = c.emitido_em
                }).ToList();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar certificados");
                return StatusCode(500, new { mensagem = "Não foi possível listar os certificados" });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try
            {
                var item = _context.Certificados
                    .Include(c => c.usuario)
                    .Include(c => c.treinamento)
                    .FirstOrDefault(c => c.id == id);

                if (item == null) return NotFound();

                var resultado = new
                {
                    id = item.id,
                    usuario = new UsuarioDTO
                    {
                        id = item.usuario.id,
                        email = item.usuario.email,
                        ativo = item.usuario.ativo,
                        nome = item.usuario.nome
                    },
                    treinamento = item.treinamento,
                    codigo = item.codigo,
                    emitido_em = item.emitido_em
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter certificado {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível obter o certificado" });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Certificados dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
                var treinoExists = _context.Treinamentos.Any(t => t.id == dto.treinamento_id);
                if (!userExists || !treinoExists)
                    return BadRequest(new { sucesso = false, message = "Usuário ou Treinamento inexistente" });

                var novo = new Certificados
                {
                    usuario_id = dto.usuario_id,
                    treinamento_id = dto.treinamento_id,
                    codigo = dto.codigo,
                    emitido_em = dto.emitido_em == default ? DateTime.UtcNow : dto.emitido_em
                };

                _context.Certificados.Add(novo);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar certificado");
                return StatusCode(500, new { mensagem = "Não foi possível criar o certificado" });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Certificados dto)
        {
            try
            {
                var item = _context.Certificados.FirstOrDefault(c => c.id == id);
                if (item == null) return NotFound();
                if (dto == null) return BadRequest();

                item.codigo = dto.codigo;
                item.emitido_em = dto.emitido_em;
                item.usuario_id = dto.usuario_id;
                item.treinamento_id = dto.treinamento_id;

                _context.SaveChanges();

                return Ok(new { sucesso = true, message = "Certificado atualizado", data = item });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar certificado {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível editar o certificado" });
            }
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _context.Certificados.FirstOrDefault(c => c.id == id);
                if (item == null) return NotFound();

                _context.Certificados.Remove(item);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar certificado {Id}", id);
                return StatusCode(500, new { mensagem = "Não foi possível deletar o certificado" });
            }
        }
    }
}