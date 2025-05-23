using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Agente vai utilizar a biblioteca MVC do ASP.NET
using Microsoft.AspNetCore.Mvc;
using Aula04_ASPNETCORE.Models;
using Aula04_ASPNETCORE.Database;
using Microsoft.EntityFrameworkCore;

namespace Aula04_ASPNETCORE.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class SoftwaresController : ControllerBase
    {
        private readonly AppDbContext _context; //readonly é uma variável que só pode ser inicializada no construtor, o AppDbContext é a classe que representa o banco de dados

        public SoftwaresController(AppDbContext context) // Construtor que recebe o AppDbContext que é a classe que representa o banco de dados
        {
            _context = context;
        }

        [HttpGet] // Define que esse método é um GET
        public async Task<IEnumerable<Softwares>> Get() // Retorna uma lista de usuários
        {
            // await é uma palavra chave que só pode ser usada em métodos que são marcados com async
            return await _context.Softwares.ToListAsync(); // Retorna todos os usuários do banco de dados
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Softwares>> GetById(int id)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null) return NotFound();

            return software;
        } // Get não está funcionando! will do futuro



        [HttpPost] // Define que esse método é um POST
        public async Task<ActionResult<Softwares>> Post([FromBody] Softwares softwares) // Task é um método assíncrono, ActionResult é o tipo de retorno do método, [FromBody] indica que o usuário vai ser passado no corpo da requisição
        {
            _context.Softwares.Add(softwares); // Adiciona o usuário no banco de dados
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return softwares; // Retorna o usuário que foi adicionado
        }

        [HttpPut("{id}")] // Define que esse método é um PUT, {id} é um parâmetro que vai ser passado na URL
        public async Task<ActionResult<Softwares>> Put(int id, [FromBody] Softwares softwares) // Task é um método assíncrono, ActionResult é o tipo de retorno do método, [FromBody] indica que o usuário vai ser passado no corpo da requisição
        {
            var existente = await _context.Softwares.FindAsync(id); // Procura o usuário no banco de dados
            if (existente == null) return NotFound(); // Se não encontrar o usuário, retorna um erro 404
            existente.Produto = softwares.Produto; // Atualiza o nome do usuário
            existente.HardDisk = softwares.HardDisk; // Atualiza o email do usuário
            existente.Memoria_Ram = softwares.Memoria_Ram;
            existente.Fk_Maquina = softwares.Fk_Maquina;

            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return existente; // Retorna o usuário que foi atualizado

        }

        [HttpDelete("{id}")] // Define que esse método é um DELETE, {id} é um parâmetro que vai ser passado na URL
        public async Task<ActionResult> Delete(int id) // Task é um método assíncrono, ActionResult é o tipo de retorno do método
        {
            var existente = await _context.Softwares.FindAsync(id); // Procura o usuário no banco de dados
            if (existente == null) return NotFound(); // Se não encontrar o usuário, retorna um erro 404
            _context.Softwares.Remove(existente); // Remove o usuário do banco de dados
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna um status 204
        }
    }
}