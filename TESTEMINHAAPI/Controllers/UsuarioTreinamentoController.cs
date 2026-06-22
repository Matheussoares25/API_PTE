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
    public class UsuarioTreinamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioTreinamentoController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            // Busca todos os registros do usuário pelo UsuarioId
            var usuarioTreinos = _context.UsuarioTreinamentos
                .Include(ut => ut.Usuario)
                .Include(ut => ut.Treinamento)
                .Where(ut => ut.UsuarioId == id)
                .ToList();
            // Retorna lista (pode ser vazia) em vez de 404, para facilitar o consumo pelo cliente
            return Ok(usuarioTreinos);
        }

       

        [HttpPost]
        public IActionResult Criar(UsuarioTreinamento dto)
        {
            if (dto == null) return BadRequest();

            var usuarioExiste = _context.Usuarios.Any(u => u.Id == dto.UsuarioId);
            var treinamentoExiste = _context.Treinamentos.Any(t => t.Id == dto.TreinamentoId);

            if (!usuarioExiste || !treinamentoExiste)
            {
                return BadRequest(new { sucesso = false, message = "Usuário ou Treinamento inexistente" });
            }

            // Evita duplicatas (mesmo UsuarioId e TreinamentoId)
            var jaExiste = _context.UsuarioTreinamentos.Any(ut => ut.UsuarioId == dto.UsuarioId && ut.TreinamentoId == dto.TreinamentoId);
            if (jaExiste)
            {
                return BadRequest(new { sucesso = false, message = "Relacionamento já existe" });
            }

            var novo = new UsuarioTreinamento
            {
                UsuarioId = dto.UsuarioId,
                TreinamentoId = dto.TreinamentoId
            };

            _context.UsuarioTreinamentos.Add(novo);
            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Usuário-Treinamento criado com sucesso", data = novo });
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, UsuarioTreinamento dto)
        {
            var item = _context.UsuarioTreinamentos.FirstOrDefault(ut => ut.Id == id);
            if (item == null) return NotFound();

            item.UsuarioId = dto.UsuarioId;
            item.TreinamentoId = dto.TreinamentoId;

            _context.SaveChanges();

            return Ok(new { successo = true, message = "Usuário-Treinamento atualizado com sucesso", data = item });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.UsuarioTreinamentos.FirstOrDefault(ut => ut.Id == id);
            if (item == null) return NotFound();

            _context.UsuarioTreinamentos.Remove(item);
            _context.SaveChanges();

            return Ok(new { successo = true, message = "Usuário-Treinamento apagado" });
        }
    }
}
