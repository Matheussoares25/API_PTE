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

            var resultado = list.Select(r => new
            {
                id = r.id,
                usuario = new UsuarioDTO
                {
                    id = r.usuario.id,
                    email = r.usuario.email,
                    ativo = r.usuario.ativo,
                    nome = r.usuario.nome
                },
                mensagem = r.mensagem,
                tipo = r.tipo,
                criado = r.criado
            }).ToList();

            return Ok(resultado);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Reports
                .Include(r => r.usuario)
                .FirstOrDefault(r => r.id == id);
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
                mensagem = item.mensagem,
                tipo = item.tipo,
                criado = item.criado
            };

            return Ok(resultado);
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

        [Authorize(Roles = "2,3")]
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
