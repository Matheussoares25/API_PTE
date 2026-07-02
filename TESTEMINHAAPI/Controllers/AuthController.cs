using Microsoft.AspNetCore.Mvc;
using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.BancoDeDados;
using TESTEMINHAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using System.Security.Cryptography;
using APIPTE.DTOS;

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
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.email == loginDto.email);

                if (usuario == null)
                {
                    return NotFound (new { successo = false, message = "Email não encontrado." });
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
                var licenca = _context.Licencas
                    .FirstOrDefault(l => l.usuario_id == usuario.id);

                if (licenca == null)
                {
                    return StatusCode(403, new
                    {
                        successo = false,
                        message = "Nenhuma licença atribuída ao usuário."
                    });
                }

                var licencaValida = licenca.ativo && licenca.validade_em > DateTime.UtcNow;

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
                        usuario.tipo,
                    },
                    Token = novoToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { successo = false, message = "Ocorreu um erro durante o login.", erro = ex.Message });
            }

        }


        [EndpointDescription("Não requer token JWT. Retorna um JWT válido após autenticação.")]
        [HttpPut("AtualizarAcesso")]
        public IActionResult AtualizarAcesso(LoginDto user)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, new { successo = false, message = "Ocorreu um erro ao atualizar o acesso.", erro = ex.Message });
            }
        }

    }
}
