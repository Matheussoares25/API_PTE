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
    public class NotasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.Notas
                .Include(n => n.Usuario)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Notas
                .Include(n => n.Usuario)
                .FirstOrDefault(n => n.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Notas dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            if (!userExists) return BadRequest(new { sucesso = false, message = "Usuário inexistente" });

            var novo = new Notas
            {
                UsuarioId = dto.UsuarioId,
                ProvaId = dto.ProvaId,
                TreinamentoId = dto.TreinamentoId,
                Valor = dto.Valor,
                Criado = DateTime.UtcNow
            };

            _context.Notas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Notas dto)
        {
            var item = _context.Notas.FirstOrDefault(n => n.Id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.Valor = dto.Valor;
            item.ProvaId = dto.ProvaId;
            item.TreinamentoId = dto.TreinamentoId;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Nota atualizada", data = item });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Notas.FirstOrDefault(n => n.Id == id);
            if (item == null) return NotFound();

            _context.Notas.Remove(item);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Nota apagada" });
        }
    }
}
