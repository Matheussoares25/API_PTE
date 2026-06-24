using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Models;
using System.Linq;
using System;
using System.Security.Cryptography;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "3")]
    public class LicencasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LicencasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/licencas
        [HttpGet]
        public IActionResult Listar()
        {
            var list = _context.Licencas
                .ToList();

            return Ok(list);
        }

        // GET: api/licencas/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult PorUsuario(int usuarioId)
        {
            var list = _context.Licencas
                .Where(l => l.usuario_id == usuarioId)
                .ToList();

            if (list == null || list.Count == 0) return NotFound();
            return Ok(list);
        }

        // GET: api/licencas/{id}
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var item = _context.Licencas.FirstOrDefault(l => l.id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/licencas
        // Requer role 3 (admin) para criar licenças
        
        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar([FromBody] Licencas dto)
        {
            if (dto == null) return BadRequest(new { message = "Dados inválidos." });

            // gera token automático de 64 caracteres (32 bytes -> 64 hex chars)
            string token;
            do
            {
                token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
            } while (_context.Licencas.Any(l => l.token == token));

            var novo = new Licencas
            {
                // aceita campos opcionais do dto; aplica valores padrão quando ausentes
                usuario_id = dto.usuario_id,
                token = token,
                criado_em = dto.criado_em == default(DateTime) ? DateTime.UtcNow : dto.criado_em,
                validade_em = dto.validade_em == default(DateTime) ? DateTime.UtcNow.AddYears(1) : dto.validade_em,
                ativo = dto.ativo,
                preco = dto.preco
            };

            _context.Licencas.Add(novo);
            _context.SaveChanges();

            // se a licença foi atribuída a um usuário, atualiza o campo token do usuário
            if (novo.usuario_id.HasValue)
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.id == novo.usuario_id.Value);
                if (usuario != null)
                {
                    usuario.token = novo.token;
                    _context.SaveChanges();
                }
            }

            return CreatedAtAction(nameof(Obter), new { id = novo.id }, novo);
        }

        // PUT: api/licencas/{id}
        // Atualiza parâmetros da licença. Requer role 3 (admin).
        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Licencas dto)
        {
            var item = _context.Licencas.FirstOrDefault(l => l.id == id);
            if (item == null) return NotFound();
            if (dto == null) return BadRequest(new { message = "Dados inválidos." });

            // token não pode ser alterado via PUT
            item.usuario_id = dto.usuario_id;
            // atualiza validade, ativo e preço
            item.validade_em = dto.validade_em;
            item.ativo = dto.ativo;
            item.preco = dto.preco;

            _context.SaveChanges();

            return Ok(new { sucesso = true, message = "Licença atualizada com sucesso", data = item });
        }

        // DELETE: api/licencas/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Licencas.FirstOrDefault(l => l.id == id);
            if (item == null) return NotFound();

            _context.Licencas.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
