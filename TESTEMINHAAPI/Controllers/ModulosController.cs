using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModulosController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var modulos = _context.Modulos
                .Include(m => m.Treinamento)
                .ToList();

            return Ok(modulos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var modulo = _context.Modulos
                .Include(m => m.Treinamento)
                .FirstOrDefault(m => m.Id == id);

            if (modulo == null) return NotFound();

            return Ok(modulo);
        }

        //[Authorize]
        [HttpGet("treinamento/{treinamentoId}")]
        public IActionResult ObterPorTreinamento(int treinamentoId)
        {
            var modulos = _context.Modulos
                .Include(m => m.Treinamento)
                .Where(m => m.TreinamentoId == treinamentoId)
                .ToList();

  
            return Ok(modulos);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Criar(Modulos dto)
        {
            if (dto == null) return BadRequest();

            var treinoExiste = _context.Treinamentos.Any(t => t.Id == dto.TreinamentoId);
            if (!treinoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Treinamento inexistente" });
            }

            var novo = new Modulos
            {
                Nome = dto.Nome,
                TreinamentoId = dto.TreinamentoId
            };

            _context.Modulos.Add(novo);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Módulo criado com sucesso", data = novo });
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Modulos dto)
        {
            var modulo = _context.Modulos.FirstOrDefault(m => m.Id == id);
            if (modulo == null) return NotFound();

            if (dto == null) return BadRequest();

            var treinoExiste = _context.Treinamentos.Any(t => t.Id == dto.TreinamentoId);
            if (!treinoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Treinamento inexistente" });
            }

            modulo.Nome = dto.Nome;
            modulo.TreinamentoId = dto.TreinamentoId;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Módulo atualizado com sucesso", data = modulo });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var modulo = _context.Modulos.FirstOrDefault(m => m.Id == id);
            if (modulo == null) return NotFound();

            _context.Modulos.Remove(modulo);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Módulo apagado" });
        }
    }
}
