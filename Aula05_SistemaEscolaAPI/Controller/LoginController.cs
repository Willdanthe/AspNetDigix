using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Aula05_SistemaEscolaAPI.DTO;
using Aula05_SistemaEscolaAPI.Models;
using Aula05_SistemaEscolaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Aula05_SistemaEscolaAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        #region  POST LOGIN
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            // IActionResult é uma interface que representa o resultado de uma ação em controlador no AspNet

            // Verifica se a string é nuça ou contém apenas espaços em brancos
            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Usuário e senha são campos obrigatórios!"); // Retorna erro 400
            }

            var users = new List<Usuario>
            {
                new() { Username = "admin", Password = "123", Role = "Administrados"},
                new() { Username = "func", Password = "123", Role = "Funcionário"},
                new() { Username = "will", Password = "will"}
            };

            var user = users.FirstOrDefault( u => 
                u.Username == loginDto.Username &&
                u.Password == loginDto.Password
            );

            if (user == null)
            {
                return Unauthorized(new {mensagem = "Usuário ou senha inválida"});
            }

            var token = TokenService.GenerateToken(user);

            return Ok(new {token});
        }
        #endregion
    }
}