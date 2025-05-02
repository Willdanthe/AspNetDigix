using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula05_SistemaEscolaAPI.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace Aula05_SistemaEscolaAPI.Services
{
    public class TokenService
    {
        public static string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("minha-chave-é-ultra-segura-com-32-caracteres");
            // Chave secreta para assinar o token. Deve ser mantida em segredo e não deve ser exposta

            var tokenDescriptor = new SecurityTokenDescriptor{
                // Essa é uma classe que contém as informações sobre o Token, aqui pode colocar o tempo de expirar o Token

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, usuario.Username),
                    new (ClaimTypes.Role, usuario.Role)
                })
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }
        
        
    }
}