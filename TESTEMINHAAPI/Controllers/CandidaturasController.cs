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
    public class CandidaturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CandidaturasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var candidaturas = _context.Candidaturas
                .Include(c => c.Vaga)
                .Include(c => c.Usuario)
                .ToList();

            return Ok(candidaturas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var cand = _context.Candidaturas
                .Include(c => c.Vaga)
                .Include(c => c.Usuario)
                .FirstOrDefault(c => c.Id == id);

            if (cand == null) return NotFound();

            return Ok(cand);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Candidaturas dto)
        {
            if (dto == null) return BadRequest();

            var vagaExiste = _context.Vagas.Any(v => v.Id == dto.VagaId);
            if (!vagaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Vaga inexistente" });
            }

            if (dto.UsuarioId.HasValue)
            {
                var userExiste = _context.Usuarios.Any(u => u.Id == dto.UsuarioId.Value);
                if (!userExiste)
                {
                    return BadRequest(new { sucesso = false, message = "Usuário informado não existe" });
                }
            }

            var novo = new Candidaturas
            {
                VagaId = dto.VagaId,
                UsuarioId = dto.UsuarioId,
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                CurriculoUrl = dto.CurriculoUrl,
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "pendente" : dto.Status,
                Criado = DateTime.UtcNow
            };

            _context.Candidaturas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Candidaturas dto)
        {
            var cand = _context.Candidaturas.FirstOrDefault(c => c.Id == id);
            if (cand == null) return NotFound();

            if (dto == null) return BadRequest();

            var vagaExiste = _context.Vagas.Any(v => v.Id == dto.VagaId);
            if (!vagaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Vaga inexistente" });
            }

            if (dto.UsuarioId.HasValue)
            {
                var userExiste = _context.Usuarios.Any(u => u.Id == dto.UsuarioId.Value);
                if (!userExiste)
                {
                    return BadRequest(new { sucesso = false, message = "Usuário informado não existe" });
                }
            }

            cand.VagaId = dto.VagaId;
            cand.UsuarioId = dto.UsuarioId;
            cand.Nome = dto.Nome;
            cand.Email = dto.Email;
            cand.Telefone = dto.Telefone;
            cand.CurriculoUrl = dto.CurriculoUrl;
            cand.Status = dto.Status;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Candidatura atualizada com sucesso", data = cand });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cand = _context.Candidaturas.FirstOrDefault(c => c.Id == id);
            if (cand == null) return NotFound();

            _context.Candidaturas.Remove(cand);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Candidatura apagada" });
        }
    }
}
