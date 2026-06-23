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
    public class CertificadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CertificadosController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.Certificados
                .Include(c => c.usuario)
                .Include(c => c.treinamento)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Certificados
                .Include(c => c.usuario)
                .Include(c => c.treinamento)
                .FirstOrDefault(c => c.id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Certificados dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
            var treinoExists = _context.Treinamentos.Any(t => t.id == dto.treinamento_id);
            if (!userExists || !treinoExists) return BadRequest(new { sucesso = false, message = "Usuário ou Treinamento inexistente" });

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

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Certificados dto)
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

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Certificados.FirstOrDefault(c => c.id == id);
            if (item == null) return NotFound();

            _context.Certificados.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
