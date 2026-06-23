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
                .Include(c => c.Usuario)
                .Include(c => c.Treinamento)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Certificados
                .Include(c => c.Usuario)
                .Include(c => c.Treinamento)
                .FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Certificados dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            var treinoExists = _context.Treinamentos.Any(t => t.Id == dto.TreinamentoId);
            if (!userExists || !treinoExists) return BadRequest(new { sucesso = false, message = "Usuário ou Treinamento inexistente" });

            var novo = new Certificados
            {
                UsuarioId = dto.UsuarioId,
                TreinamentoId = dto.TreinamentoId,
                Codigo = dto.Codigo,
                EmitidoEm = dto.EmitidoEm == default ? DateTime.UtcNow : dto.EmitidoEm
            };

            _context.Certificados.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Certificados dto)
        {
            var item = _context.Certificados.FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.Codigo = dto.Codigo;
            item.EmitidoEm = dto.EmitidoEm;
            item.UsuarioId = dto.UsuarioId;
            item.TreinamentoId = dto.TreinamentoId;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Certificado atualizado", data = item });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Certificados.FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();

            _context.Certificados.Remove(item);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Certificado apagado" });
        }
    }
}
