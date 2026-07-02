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
                .Include(n => n.usuario)
                .ToList();

            var resultado = list.Select(n => new
            {
                id = n.id,
                usuario = new UsuarioDTO
                {
                    id = n.usuario.id,
                    email = n.usuario.email,
                    ativo = n.usuario.ativo,
                    nome = n.usuario.nome
                },
                prova_id = n.prova_id,
                treinamento_id = n.treinamento_id,
                valor = n.valor,
                criado = n.criado
            }).ToList();

            return Ok(resultado);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Notas
                .Include(n => n.usuario)
                .FirstOrDefault(n => n.id == id);
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
                prova_id = item.prova_id,
                treinamento_id = item.treinamento_id,
                valor = item.valor,
                criado = item.criado
            };

            return Ok(resultado);
        }

        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Notas dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
            if (!userExists) return BadRequest(new { sucesso = false, message = "Usuário inexistente" });

            var novo = new Notas
            {
                usuario_id = dto.usuario_id,
                prova_id = dto.prova_id,
                treinamento_id = dto.treinamento_id,
                valor = dto.valor,
                criado = DateTime.UtcNow
            };

            _context.Notas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Notas dto)
        {
            var item = _context.Notas.FirstOrDefault(n => n.id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.valor = dto.valor;
            item.prova_id = dto.prova_id;
            item.treinamento_id = dto.treinamento_id;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Nota atualizada", data = item });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Notas.FirstOrDefault(n => n.id == id);
            if (item == null) return NotFound();

            _context.Notas.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
