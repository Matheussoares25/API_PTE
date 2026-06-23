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
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.Reports
                .Include(r => r.Usuario)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Reports
                .Include(r => r.Usuario)
                .FirstOrDefault(r => r.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // salva reports dos usuarios
        [Authorize]
        [HttpPost]
        public IActionResult Criar(Reports dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            if (!userExists) return BadRequest(new { sucesso = false, message = "Usuário inexistente" });

            var novo = new Reports
            {
                UsuarioId = dto.UsuarioId,
                Mensagem = dto.Mensagem,
                Tipo = dto.Tipo,
                Criado = DateTime.UtcNow
            };

            _context.Reports.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Reports dto)
        {
            var item = _context.Reports.FirstOrDefault(r => r.Id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.Mensagem = dto.Mensagem;
            item.Tipo = dto.Tipo;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Report atualizado", data = item });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Reports.FirstOrDefault(r => r.Id == id);
            if (item == null) return NotFound();

            _context.Reports.Remove(item);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Report apagado" });
        }
    }
}
