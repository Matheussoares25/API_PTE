using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TESTEMINHAAPI.Models;

namespace TESTEMINHAAPI.Services
{
    public class TokenService
    {
        public string GerarToken(Usuario user)
        {
            var tokengerado = new JwtSecurityTokenHandler();

            var chave = Encoding.UTF8.GetBytes("MinhaChaveJWTMuitoSeguraComMaisDe32Caracteres");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.id.ToString()),
                    new Claim("Nome", user.nome),
                    new Claim("Email", user.email),
                    new Claim(ClaimTypes.Role, user.tipo.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokengerado.CreateToken(tokenDescriptor);

            return tokengerado.WriteToken(token);

        }
    }
}
