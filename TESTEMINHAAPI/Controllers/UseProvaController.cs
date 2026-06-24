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

            var resultado = list.Select(u => new
            {
                id = u.id,
                usuario = new UsuarioDTO
                {
                    id = u.usuario.id,
                    email = u.usuario.email,
                    ativo = u.usuario.ativo,
                    nome = u.usuario.nome
                },
                prova_id = u.prova_id,
                nota = u.nota,
                realizado_em = u.realizado_em
            }).ToList();

            return Ok(resultado);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.UseProva
                .Include(u => u.usuario)
                .FirstOrDefault(u => u.id == id);
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
                nota = item.nota,
                realizado_em = item.realizado_em
            };

            return Ok(resultado);
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

        [Authorize(Roles = "2,3")]
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
