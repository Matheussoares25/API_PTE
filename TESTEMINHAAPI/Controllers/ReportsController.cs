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
                .Include(r => r.usuario)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Reports
                .Include(r => r.usuario)
                .FirstOrDefault(r => r.id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // salva reports dos usuarios
        [Authorize]
        [HttpPost]
        public IActionResult Criar(Reports dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
            if (!userExists) return BadRequest(new { sucesso = false, message = "Usuário inexistente" });

            var novo = new Reports
            {
                usuario_id = dto.usuario_id,
                mensagem = dto.mensagem,
                tipo = dto.tipo,
                criado = DateTime.UtcNow
            };

            _context.Reports.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Reports dto)
        {
            var item = _context.Reports.FirstOrDefault(r => r.id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.mensagem = dto.mensagem;
            item.tipo = dto.tipo;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Report atualizado", data = item });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Reports.FirstOrDefault(r => r.id == id);
            if (item == null) return NotFound();

            _context.Reports.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
