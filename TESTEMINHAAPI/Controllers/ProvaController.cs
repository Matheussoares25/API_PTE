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
    public class ProvaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProvaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as provas existentes.
        /// </summary>
        /// <remarks>
        /// Retorna 200 com a lista de provas incluindo dados do Treinamento e Questões.
        /// Requer autorização.
        /// </remarks>
        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var provas = _context.Provas
                .Include(p => p.treinamento)
                .Include(p => p.questoes)
                .ToList();

            return Ok(provas);
        }

        /// <summary>
        /// Obtém uma prova pelo seu Id.
        /// </summary>
        /// <remarks>
        /// Inclui dados do Treinamento e Questões. Retorna 200 com a prova ou 404 se não encontrado. Requer autorização.
        /// </remarks>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var prova = _context.Provas
                .Include(p => p.treinamento)
                .Include(p => p.questoes)
                .FirstOrDefault(p => p.id == id);

            if (prova == null) return NotFound();

            return Ok(prova);
        }

        /// <summary>
        /// Lista provas pertencentes a um Treinamento específico.
        /// </summary>
        /// <remarks>
        /// Retorna 200 com a lista de provas (pode ser vazia). Requer autorização.
        /// </remarks>
        [Authorize]
        [HttpGet("treinamento/{treinamentoId}")]
        public IActionResult ObterPorTreinamento(int treinamentoId)
        {
            var provas = _context.Provas
                .Include(p => p.treinamento)
                .Include(p => p.questoes)
                .Where(p => p.treinamento_id == treinamentoId)
                .ToList();

            return Ok(provas);
        }

        /// <summary>
        /// Cria uma nova prova para um Treinamento.
        /// </summary>
        /// <remarks>
        /// Valida existência do Treinamento; retorna 400 em caso de erro ou 201 com o objeto criado. Requer autorização (Roles: 2 ou 3).
        /// </remarks>
        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Prova dto)
        {
            if (dto == null) return BadRequest();

            var treinamentoExiste = _context.Treinamentos.Any(t => t.id == dto.treinamento_id);
            if (!treinamentoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Treinamento inexistente" });
            }

            var novaProva = new Prova
            {
                titulo = dto.titulo,
                descricao = dto.descricao,
                treinamento_id = dto.treinamento_id,
                pontuacao_maxima = dto.pontuacao_maxima,
                status = dto.status ?? "ativa",
                criado = DateTime.Now
            };

            _context.Provas.Add(novaProva);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = novaProva.id }, novaProva);
        }

        /// <summary>
        /// Atualiza uma prova existente pelo Id.
        /// </summary>
        /// <remarks>
        /// Valida existência da prova e do Treinamento; retorna 404 se não existir, 400 para dados inválidos e 200 em caso de sucesso. Requer autorização (Roles: 2 ou 3).
        /// </remarks>
        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Prova dto)
        {
            var prova = _context.Provas.FirstOrDefault(p => p.id == id);
            if (prova == null) return NotFound();

            if (dto == null) return BadRequest();

            var treinamentoExiste = _context.Treinamentos.Any(t => t.id == dto.treinamento_id);
            if (!treinamentoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Treinamento inexistente" });
            }

            prova.titulo = dto.titulo;
            prova.descricao = dto.descricao;
            prova.treinamento_id = dto.treinamento_id;
            prova.pontuacao_maxima = dto.pontuacao_maxima;
            prova.status = dto.status ?? prova.status;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Prova atualizada com sucesso", data = prova });
        }

        /// <summary>
        /// Exclui uma prova pelo Id.
        /// </summary>
        /// <remarks>
        /// Requer autorização (Role: 3). Retorna 404 se a prova não existir e 204 em caso de sucesso.
        /// </remarks>
        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prova = _context.Provas.FirstOrDefault(p => p.id == id);
            if (prova == null) return NotFound();

            _context.Provas.Remove(prova);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
