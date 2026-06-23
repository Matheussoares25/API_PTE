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
            var vaga = _context.Vagas.FirstOrDefault(v => v.Id == id);
            if (vaga == null) return NotFound();
            return Ok(vaga);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Vagas dto)
        {
            if (dto == null) return BadRequest();

            var novo = new Vagas
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Localizacao = dto.Localizacao,
                Quantidade = dto.Quantidade,
                Ativa = dto.Ativa,
                Criado = DateTime.UtcNow
            };

            _context.Vagas.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novo.Id }, novo);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Vagas dto)
        {
            var vaga = _context.Vagas.FirstOrDefault(v => v.Id == id);
            if (vaga == null) return NotFound();

            if (dto == null) return BadRequest();

            vaga.Titulo = dto.Titulo;
            vaga.Descricao = dto.Descricao;
            vaga.Localizacao = dto.Localizacao;
            vaga.Quantidade = dto.Quantidade;
            vaga.Ativa = dto.Ativa;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Vaga atualizada com sucesso", data = vaga });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vaga = _context.Vagas.FirstOrDefault(v => v.Id == id);
            if (vaga == null) return NotFound();

            _context.Vagas.Remove(vaga);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Vaga apagada" });
        }
    }
}
