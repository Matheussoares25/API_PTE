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
    public class UseTreinamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UseTreinamentosController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.UseTreinamentos
                .Include(u => u.Usuario)
                .Include(u => u.Treinamento)
                .ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.UseTreinamentos
                .Include(u => u.Usuario)
                .Include(u => u.Treinamento)
                .FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(UseTreinamentos dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            var treinoExists = _context.Treinamentos.Any(t => t.Id == dto.TreinamentoId);
            if (!userExists || !treinoExists) return BadRequest(new { sucesso = false, message = "Usuário ou Treinamento inexistente" });

            var novo = new UseTreinamentos
            {
                UsuarioId = dto.UsuarioId,
                TreinamentoId = dto.TreinamentoId,
                MatriculadoEm = DateTime.UtcNow,
                Status = dto.Status
            };

            _context.UseTreinamentos.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, UseTreinamentos dto)
        {
            var item = _context.UseTreinamentos.FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.Status = dto.Status;
            item.MatriculadoEm = dto.MatriculadoEm;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Registro de matrícula atualizado", data = item });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.UseTreinamentos.FirstOrDefault(u => u.Id == id);
            if (item == null) return NotFound();

            _context.UseTreinamentos.Remove(item);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Registro de matrícula apagado" });
        }
    }
}
