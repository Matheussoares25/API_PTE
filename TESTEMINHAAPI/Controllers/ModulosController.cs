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

        /// <summary>
        /// Lista todos os módulos existentes.
        /// </summary>
        /// <remarks>
        /// Retorna 200 com a lista de módulos incluindo os dados do Treinamento.
        /// Requer autorização.
        /// </remarks>
        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            var modulos = _context.Modulos
                .Include(m => m.Treinamento)
                .ToList();

            return Ok(modulos);
        }

        /// <summary>
        /// Obtém um módulo pelo seu Id.
        /// </summary>
        /// <remarks>
        /// Inclui dados do Treinamento. Retorna 200 com o módulo ou 404 se não encontrado. Requer autorização.
        /// </remarks>
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

        /// <summary>
        /// Lista módulos pertencentes a um Treinamento específico.
        /// </summary>
        /// <remarks>
        /// Retorna 200 com a lista de módulos (pode ser vazia). Não exige autorização atualmente.
        /// </remarks>
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

        /// <summary>
        /// Cria um novo módulo para um Treinamento.
        /// </summary>
        /// <remarks>
        /// Valida existência do Treinamento; retorna 400 em caso de erro ou 200 com o objeto criado. Requer autorização.
        /// </remarks>
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

        /// <summary>
        /// Atualiza um módulo existente pelo Id.
        /// </summary>
        /// <remarks>
        /// Valida existência do módulo e do Treinamento; retorna 404 se não existir, 400 para dados inválidos e 200 em caso de sucesso. Requer autorização.
        /// </remarks>
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

        /// <summary>
        /// Exclui um módulo pelo Id.
        /// </summary>
        /// <remarks>
        /// Requer autorização. Retorna 404 se o módulo não existir e 200 em caso de sucesso.
        /// </remarks>
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
