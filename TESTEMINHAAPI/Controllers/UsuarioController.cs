using Microsoft.AspNetCore.Mvc;

using TESTEMINHAAPI.Models;
using Microsoft.AspNetCore.Identity;
using TESTEMINHAAPI.BancoDeDados;
using Microsoft.AspNetCore.Authorization;


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
        public ActionResult Listar()
        {
            var usuarios = _context.Usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == id);

            if (usuario == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }

            return Ok(new
            {
                usuario.id,
                usuario.email,
                usuario.tipo,
               
            });
        }

        [Authorize(Roles = "3")]
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == id);
            if (usuario == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Usuario usuarioAtualizado)
        {
            var UsuarioExistente = _context.Usuarios.FirstOrDefault(u => u.id == id);
            if (UsuarioExistente == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }
            UsuarioExistente.nome = usuarioAtualizado.nome;
            UsuarioExistente.email = usuarioAtualizado.email;
            _context.SaveChanges();
            return Ok(UsuarioExistente);
        }

        [EndpointDescription("Não requer token JWT. Retorna um JWT válido após autenticação.")]
        [HttpPost]
        public IActionResult Criar([FromBody] Usuario dto)
        {
            if (dto == null) return BadRequest(new { Message = "Dados do usuário inválidos." });

            if (string.IsNullOrWhiteSpace(dto.senha))
                return BadRequest(new { Message = "Senha é obrigatória." });

            // Valida e evita duplicação de email
            var existe = _context.Usuarios.Any(u => u.email == dto.email);
            if (existe)
            {
                return BadRequest(new { Message = "Email já cadastrado." });
            }

            var novo = new Usuario
            {
                nome = dto.nome,
                email = dto.email,
                ativo = dto.ativo != 0 ? dto.ativo : 1,
                token = dto.token,
                tipo = dto.tipo,
                acesso = dto.acesso
            };

            // Hash da senha antes de persistir - evita FormatException ao verificar posteriormente
            var hasher = new PasswordHasher<Usuario>();
            novo.senha = hasher.HashPassword(novo, dto.senha);

            _context.Usuarios.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novo.id }, novo);
        }
    }
}