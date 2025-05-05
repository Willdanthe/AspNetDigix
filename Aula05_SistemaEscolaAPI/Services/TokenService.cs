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
                    // CLaimsIdentity é uma classe que representa uma identidade do usuário com um conjunto de reivindicações
                    new (ClaimTypes.Name, usuario.Username),
                    // Uma reividicação (claim) é uma declaração sobre um usuário, com seu nome ou função
                    
                    // new (ClaimTypes.Role, usuario.Role)
                    
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Aqui está definindo o tempo de expiração do token, que vai ficar duas horas a partir do momento que foi chamado

                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(key),
                // é uma classe que representa uma chave simétreica usada para assinatura do token
                SecurityAlgorithms.HmacSha256Signature), // É bom colocar no próximo desafio-***********************************
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
            // WriteToken, é um método que serializa o token JWT em string
            // Serializar é o processo de converter um objeto em uma representação string ou binária de armazenamento ou transmissão
            // O token deve ser enviado no cabeçalho da Autorização das requisições HTTP


        }
        
        
    }
}