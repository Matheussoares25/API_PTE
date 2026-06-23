using Microsoft.AspNetCore.Mvc;

using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.BancoDeDados;


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
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }
            return Ok(usuario);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
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
            var UsuarioExistente = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (UsuarioExistente == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }
            UsuarioExistente.Nome = usuarioAtualizado.Nome;
            UsuarioExistente.Email = usuarioAtualizado.Email;
            _context.SaveChanges();
            return Ok(UsuarioExistente);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Usuario dto)
        {
            if (dto == null) return BadRequest(new { Message = "Dados do usuário inválidos." });

            // Valida e evita duplicação de email
            var existe = _context.Usuarios.Any(u => u.Email == dto.Email);
            if (existe)
            {
                return BadRequest(new { Message = "Email já cadastrado." });
            }

            var novo = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                Ativo = dto.Ativo != 0 ? dto.Ativo : 1,
                Token = dto.Token,
                Tipo = dto.Tipo,
                Acesso = dto.Acesso
            };

            _context.Usuarios.Add(novo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novo.Id }, novo);
        }
    }
}