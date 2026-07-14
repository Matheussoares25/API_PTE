using Microsoft.AspNetCore.Mvc;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatriculasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.Matriculas
                .Include(u => u.treinamento)
                .Include(u => u.Usuario)
                .ToList();

            var resultado = list.Select(ut => new
            {
                id = ut.id,
                usuario = new UsuarioDTO
                {
                    id = ut.Usuario.id,
                    email = ut.Usuario.email,
                    ativo = ut.Usuario.ativo,
                    nome = ut.Usuario.nome
                },
                treinamento = ut.treinamento,
                matriculado_em = ut.matriculado_em,
                status = ut.status
            }).ToList();

            return Ok(resultado);
        }

        [HttpGet("treinamento/{treinamentoId}/")]
        public IActionResult UsuariosPorTreinamento(int treinamentoId)
        {
            var usuarios = _context.Matriculas
                .Include(ut => ut.Usuario)
                .Where(ut => ut.treinamento_id == treinamentoId)
                .Select(ut => new
                {
                    usuario = new
                    {
                        ut.Usuario.id,
                        ut.Usuario.nome,
                        ut.Usuario.email,
                        ut.Usuario.ativo
                    },

                    ut.id,
                    ut.treinamento_id,
                    ut.matriculado_em,
                    ut.status
                })
                .ToList();

            return Ok(usuarios);
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult TreinamentosPorUsuario(int usuarioId)
        {
            var treinamentos = _context.Matriculas
                .Include(ut => ut.treinamento)
                .Where(ut => ut.usuario_id == usuarioId)
                .Select(ut => ut.treinamento)
                .ToList();

            return Ok(treinamentos);
        }

        [HttpPost]
        public IActionResult Criar(Matricula dto)
        {
            if (dto == null) return BadRequest();

            var userExists = _context.Usuarios.Any(u => u.id == dto.usuario_id);
            var treinoExists = _context.Treinamentos.Any(t => t.id == dto.treinamento_id);
            if (!userExists || !treinoExists) return BadRequest(new { sucesso = false, message = "Usuário ou Treinamento inexistente" });

            var novo = new Matricula
            {
                usuario_id = dto.usuario_id,
                treinamento_id = dto.treinamento_id,
                matriculado_em = DateTime.UtcNow,
                status = dto.status
            };

            _context.Matriculas.Add(novo);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, Matricula dto)
        {
            var item = _context.Matriculas.FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest();

            item.status = dto.status;
            item.matriculado_em = dto.matriculado_em;

            _context.SaveChanges();
            return Ok(new { sucesso = true, message = "Registro de matrícula atualizado", data = item });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Matriculas.FirstOrDefault(u => u.id == id);
            if (item == null) return NotFound();

            _context.Matriculas.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
