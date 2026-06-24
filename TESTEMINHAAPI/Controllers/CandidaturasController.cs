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
                .Include(c => c.vaga)
                .Include(c => c.usuario)
                .ToList();

            return Ok(candidaturas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var cand = _context.Candidaturas
                .Include(c => c.vaga)
                .Include(c => c.usuario)
                .FirstOrDefault(c => c.id == id);

            if (cand == null) return NotFound();

            return Ok(cand);
        }

        /// <summary>
        /// Cria uma nova candidatura.
        /// </summary>
        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Candidaturas dto)
        {
            if (dto == null) return BadRequest();

            var vagaExiste = _context.Vagas.Any(v => v.id == dto.vaga_id);
            if (!vagaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Vaga inexistente" });
            }

            if (dto.usuario_id.HasValue)
            {
                var userExiste = _context.Usuarios.Any(u => u.id == dto.usuario_id.Value);
                if (!userExiste)
                {
                    return BadRequest(new { sucesso = false, message = "Usuário informado não existe" });
                }
            }

            var novo = new Candidaturas
            {
                vaga_id = dto.vaga_id,
                usuario_id = dto.usuario_id,
                nome = dto.nome,
                email = dto.email,
                telefone = dto.telefone,
                curriculo_url = dto.curriculo_url,
                status = string.IsNullOrWhiteSpace(dto.status) ? "pendente" : dto.status,
                criado = DateTime.UtcNow
            };

            _context.Candidaturas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Candidaturas dto)
        {
            var cand = _context.Candidaturas.FirstOrDefault(c => c.id == id);
            if (cand == null) return NotFound();

            if (dto == null) return BadRequest();

            var vagaExiste = _context.Vagas.Any(v => v.id == dto.vaga_id);
            if (!vagaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Vaga inexistente" });
            }

            if (dto.usuario_id.HasValue)
            {
                var userExiste = _context.Usuarios.Any(u => u.id == dto.usuario_id.Value);
                if (!userExiste)
                {
                    return BadRequest(new { sucesso = false, message = "Usuário informado não existe" });
                }
            }

            cand.vaga_id = dto.vaga_id;
            cand.usuario_id = dto.usuario_id;
            cand.nome = dto.nome;
            cand.email = dto.email;
            cand.telefone = dto.telefone;
            cand.curriculo_url = dto.curriculo_url;
            cand.status = dto.status;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Candidatura atualizada com sucesso", data = cand });
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cand = _context.Candidaturas.FirstOrDefault(c => c.id == id);
            if (cand == null) return NotFound();

            _context.Candidaturas.Remove(cand);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
