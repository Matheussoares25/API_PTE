using Microsoft.AspNetCore.Mvc;
using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using System.Security.Cryptography;

namespace TESTEMINHAAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService token)
        {
            _context = context;
            _tokenService = token;
        }



        private string GerarToken()
        {
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            return token;
        }

        [EndpointDescription("Não requer token JWT. Retorna um JWT válido após autenticação.")]
        [HttpPost("login")]

        public IActionResult Login(LoginDto loginDto)
        {

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == loginDto.Email);

            if (usuario == null)
            {
                return Unauthorized(new { successo = false, Message = "Email não encontrado." });
            }

            var hash = new PasswordHasher<Usuario>();

            var result = hash.VerifyHashedPassword(usuario, usuario.Senha, loginDto.Senha);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { successo = false , Message = "senha Invalida." });
            }

            if(usuario.Acesso == 0)
            {
                return Unauthorized(new { PrimeiroAcesso = true, Message = "aceite os Termos para prosseguir" });
            }

            var novoToken = _tokenService.GerarToken(usuario);
           

            return Ok(new
            {
                successo = true,
                mensagem = "Login realizado com sucesso",
                usuario = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Acesso,
                },
                Token = novoToken
            });


        }


        [EndpointDescription("Não requer token JWT. Retorna um JWT válido após autenticação.")]
        [HttpPut("AtualizarAcesso")]
        public IActionResult AtualizarAcesso(Usuario user)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == user.Email);

            if(usuario == null)
            {
                return Unauthorized(new { successo = false, Message = "Email não encontrado." });
            }

            usuario.Acesso = 1;

            _context.SaveChanges();

            return Ok(new { successo = true, Message = "Acesso Liberado" });
        }



        [EndpointDescription("Não requer token JWT. Retorna um JWT válido após autenticação.")]
        [HttpPost("Cadastrar")]
        public IActionResult Cadastar(Usuario user)
        {
            var senhaHash = new PasswordHasher<Usuario>();

            user.Senha = senhaHash.HashPassword(user, user.Senha);

            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
