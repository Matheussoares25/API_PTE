using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Security.Claims;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.DTOS;
using TESTEMINHAAPI.Models;


namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "2,3")]
        public ActionResult Listar()
        {
            try
            {
                var usuarios = _context.Usuarios
                    .Select(u => new UsuarioDTO
                    {
                        id = u.id,
                        nome = u.nome,
                        email = u.email,
                        acesso = u.acesso,
                        ativo = u.ativo
                    })
                    .ToList();

                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao listar os usuários." });
            }
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult BuscarPorId()
        {
            try
            {
                var idClaim = User.FindFirst("Id")?.Value;

                if (idClaim == null || !int.TryParse(idClaim, out int id))
                {
                    return Unauthorized(new { message = "Token inválido ou sem id." });
                }

                var usuario = _context.Usuarios.FirstOrDefault(u => u.id == id);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(new
                {
                    usuario.id,
                    usuario.email,
                    usuario.tipo,
                    usuario.nome
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao buscar o usuário.", erro = ex.Message });
            }
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.id == id);
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar o usuário.", erro = ex.Message });
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "2,3")]
        public IActionResult Atualizar(int id, [FromBody] Usuario usuarioAtualizado)
        {
            try
            {
                var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.id == id);

                if (usuarioExistente == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                usuarioExistente.nome = usuarioAtualizado.nome;
                usuarioExistente.email = usuarioAtualizado.email;
                usuarioExistente.ativo = usuarioAtualizado.ativo;
                usuarioExistente.tipo = usuarioAtualizado.tipo;

                _context.SaveChanges();

                return Ok(new UsuarioDTO
                {
                    id = usuarioExistente.id,
                    email = usuarioExistente.email,
                    nome = usuarioExistente.nome,
                    ativo = usuarioExistente.ativo,
                    acesso = usuarioExistente.acesso

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocorreu um erro ao atualizar o usuário.",
                    erro = ex.Message
                });
            }
        }

        [EndpointDescription("Não requer token JWT. Retorna um JWT válido após autenticação.")]
        [HttpPost]
        public IActionResult Criar([FromBody] Usuario dto)
        {
            try
            {
                if (dto == null) return BadRequest(new { message = "Dados do usuário inválidos." });

                if (string.IsNullOrWhiteSpace(dto.senha))
                    return BadRequest(new { message = "Senha é obrigatória." });

                // Valida e evita duplicação de email
                var existe = _context.Usuarios.Any(u => u.email == dto.email);
                if (existe)
                {
                    return BadRequest(new { message = "Email já cadastrado." });
                }

                var novo = new Usuario
                {
                    nome = dto.nome,
                    email = dto.email,
                    ativo = dto.ativo != 0 ? dto.ativo : 1,
                    tipo = dto.tipo,
                    acesso = dto.acesso
                };

                // Hash da senha antes de persistir - evita FormatException ao verificar posteriormente
                var hasher = new PasswordHasher<Usuario>();
                novo.senha = hasher.HashPassword(novo, dto.senha);

                _context.Usuarios.Add(novo);
                _context.SaveChanges();

                var retorno = new UsuarioDTO
                {
                    id = novo.id,
                    nome = novo.nome,
                    email = novo.email,
                    ativo = novo.ativo,
                };

                return CreatedAtAction(nameof(BuscarPorId), new { id = novo.id }, retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao criar o usuário.", erro = ex.Message });
            }
        }
    }
}
