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
    // Controller para gerenciar registros de matrícula/uso de treinamentos.
    // UseTreinamentos armazena metadados da matrícula (matriculado_em, status).
    // Ideal para operações que precisam de data de matrícula ou status do usuário no treinamento.
    public class UseTreinamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UseTreinamentosController(AppDbContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.UseTreinamentos
                .Include(u => u.usuario)
                .Include(u => u.treinamento)
                .ToList();
            return Ok(list);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.UseTreinamentos
                .Include(u => u.usuario)
                .Include(u => u.treinamento)
                .FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        //[Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(UseTreinamentos dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
            var treinoExists = _context.Treinamentos.Any(t => t.id == dto.treinamento_id);
            if (!userExists || !treinoExists) return BadRequest(new { sucesso = false, message = "Usuário ou Treinamento inexistente" });

            var novo = new UseTreinamentos
            {
                usuario_id = dto.usuario_id,
                treinamento_id = dto.treinamento_id,
                matriculado_em = DateTime.UtcNow,
                status = dto.status
            };

            _context.UseTreinamentos.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

       // [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, UseTreinamentos dto)
        {
            var item = _context.UseTreinamentos.FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.status = dto.status;
            item.matriculado_em = dto.matriculado_em;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Registro de matrícula atualizado", data = item });
        }

        //[Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.UseTreinamentos.FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();

            _context.UseTreinamentos.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
