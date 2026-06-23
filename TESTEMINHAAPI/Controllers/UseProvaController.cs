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
                .Include(u => u.Usuario)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.UseProva
                .Include(u => u.Usuario)
                .FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(UseProva dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            if (!userExists) return BadRequest(new { sucesso = false, message = "Usuário inexistente" });

            var novo = new UseProva
            {
                UsuarioId = dto.UsuarioId,
                ProvaId = dto.ProvaId,
                Nota = dto.Nota,
                RealizadoEm = DateTime.UtcNow
            };

            _context.UseProva.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, UseProva dto)
        {
            var item = _context.UseProva.FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.Nota = dto.Nota;
            item.ProvaId = dto.ProvaId;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Registro de prova atualizado", data = item });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.UseProva.FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();

            _context.UseProva.Remove(item);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Registro de prova apagado" });
        }
    }
}
