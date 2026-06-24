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

        // Retorna todos os usuários associados a um Treinamento específico
       // [Authorize]
        [HttpGet("treinamento/{treinamentoId}/")]
        public IActionResult UsuariosPorTreinamento(int treinamentoId)
        {
            var usuarios = _context.UseTreinamentos
                .Include(ut => ut.Usuario)
                .Where(ut => ut.treinamento_id == treinamentoId)
                .Select(ut => ut.Usuario)
                .ToList();

            return Ok(usuarios);
        }

        // Retorna todos os treinamentos associados a um Usuário (URL: usuario/{id})
      //  [Authorize]
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult TreinamentosPorUsuario(int usuarioId)
        {
            var treinamentos = _context.UseTreinamentos
                .Include(ut => ut.treinamento)
                .Where(ut => ut.usuario_id == usuarioId)
                .Select(ut => ut.treinamento)
                .ToList();

            return Ok(treinamentos);
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

            return Ok();
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
