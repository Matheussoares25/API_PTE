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
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == loginDto.email);

            if (usuario == null)
            {
                return Unauthorized(new { successo = false, message = "Email não encontrado." });
            }

            var hash = new PasswordHasher<Usuario>();

            var result = hash.VerifyHashedPassword(usuario, usuario.senha, loginDto.senha);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { successo = false , message = "senha Invalida." });
            }

            if(usuario.acesso == 0)
            {
                return Unauthorized(new { PrimeiroAcesso = true, message = "aceite os Termos para prosseguir" });
            }

            // Mantém validação: verifica se o token/licença associado ao usuário está ativo, válido e pertence ao usuário
            if (string.IsNullOrWhiteSpace(usuario.token))
            {
                return StatusCode(403, new { successo = false, message = "Nenhuma licença atribuída ao usuário." });
            }

            var licencaValida = _context.Licencas.Any(l =>
                l.token == usuario.token &&
                l.ativo &&
                l.validade_em > DateTime.UtcNow &&
                l.usuario_id == usuario.id
            );

            if (!licencaValida)
            {
                // Usuário autenticado, mas sem licença válida pertencente a ele -> 403 Forbidden
                return StatusCode(403, new { successo = false, message = "Licença inválida, expirada ou não pertence ao usuário." });
            }

            var novoToken = _tokenService.GerarToken(usuario);


            return Ok(new
            {
                successo = true,
                mensagem = "Login realizado com sucesso",
                usuario = new
                {
                    usuario.id,
                    usuario.nome,
                    usuario.email,
                    usuario.acesso,
                },
                Token = novoToken
            });


        }


        [EndpointDescription("Não requer token JWT. Retorna um JWT válido após autenticação.")]
        [HttpPut("AtualizarAcesso")]
        public IActionResult AtualizarAcesso(LoginDto user)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == user.email);

            if(usuario == null)
            {
                return Unauthorized(new { successo = false, message = "Email não encontrado." });
            }

            usuario.acesso = 1;

            _context.SaveChanges();

            return Ok(new { successo = true, message = "Acesso Liberado" });
        }
      
    }
}
