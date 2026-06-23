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
    public class UseProvaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UseProvaController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.UseProva
                .Include(u => u.usuario)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.UseProva
                .Include(u => u.usuario)
                .FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(UseProva dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
            if (!userExists) return BadRequest(new { sucesso = false, message = "Usuário inexistente" });

            var novo = new UseProva
            {
                usuario_id = dto.usuario_id,
                prova_id = dto.prova_id,
                nota = dto.nota,
                realizado_em = DateTime.UtcNow
            };

            _context.UseProva.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, UseProva dto)
        {
            var item = _context.UseProva.FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.nota = dto.nota;
            item.prova_id = dto.prova_id;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Registro de prova atualizado", data = item });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.UseProva.FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();

            _context.UseProva.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
