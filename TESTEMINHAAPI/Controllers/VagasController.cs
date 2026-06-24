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
    public class VagasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VagasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var vagas = _context.Vagas.ToList();
            return Ok(vagas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var vaga = _context.Vagas.FirstOrDefault(v => v.id == id);
            if (vaga == null) return NotFound();
            return Ok(vaga);
        }

        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Vagas dto)
        {
            if (dto == null) return BadRequest();

            var novo = new Vagas
            {
                titulo = dto.titulo,
                descricao = dto.descricao,
                localizacao = dto.localizacao,
                quantidade = dto.quantidade,
                ativa = dto.ativa,
                criado = DateTime.UtcNow
            };

            _context.Vagas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Vagas dto)
        {
            var vaga = _context.Vagas.FirstOrDefault(v => v.id == id);
            if (vaga == null) return NotFound();

            if (dto == null) return BadRequest();

            vaga.titulo = dto.titulo;
            vaga.descricao = dto.descricao;
            vaga.localizacao = dto.localizacao;
            vaga.quantidade = dto.quantidade;
            vaga.ativa = dto.ativa;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Vaga atualizada com sucesso", data = vaga });
        }
        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vaga = _context.Vagas.FirstOrDefault(v => v.id == id);
            if (vaga == null) return NotFound();

            _context.Vagas.Remove(vaga);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
