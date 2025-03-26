using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Agente vai utilizar a biblioteca MVC do ASP.NET
using Microsoft.AspNetCore.Mvc;
using Aula03_ASPNET_API_ADO.Models;
using Aula03_ASPNET_API_ADO.Database;
using Microsoft.EntityFrameworkCore;

namespace Aula03_ASPNET_API_ADO.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class MaquinasController : ControllerBase
    {
        private readonly AppDbContext _context; //readonly é uma variável que só pode ser inicializada no construtor, o AppDbContext é a classe que representa o banco de dados

        public MaquinasController(AppDbContext context) // Construtor que recebe o AppDbContext que é a classe que representa o banco de dados
        {
            _context = context;
        }

        [HttpGet] // Define que esse método é um GET
        public async Task<IEnumerable<Maquinas>> Get() // Retorna uma lista de usuários
        {
            // await é uma palavra chave que só pode ser usada em métodos que são marcados com async
            return await _context.Maquinas.ToListAsync(); // Retorna todos os usuários do banco de dados
        }

        [HttpPost] // Define que esse método é um POST
        public async Task<ActionResult<Maquinas>> Post([FromBody] Maquinas maquinas) // Task é um método assíncrono, ActionResult é o tipo de retorno do método, [FromBody] indica que o usuário vai ser passado no corpo da requisição
        {
            _context.Maquinas.Add(maquinas); // Adiciona o usuário no banco de dados
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return maquinas; // Retorna o usuário que foi adicionado
        }

        [HttpPut("{id}")] // Define que esse método é um PUT, {id} é um parâmetro que vai ser passado na URL
        public async Task<ActionResult<Maquinas>> Put(int id, [FromBody] Maquinas maquinas) // Task é um método assíncrono, ActionResult é o tipo de retorno do método, [FromBody] indica que o usuário vai ser passado no corpo da requisição
        {
            var existente = await _context.Maquinas.FindAsync(id); // Procura o usuário no banco de dados
            if (existente == null) return NotFound(); // Se não encontrar o usuário, retorna um erro 404
            existente.Tipo = maquinas.Tipo; // Atualiza o nome do usuário
            existente.Velocidade = maquinas.Velocidade; // Atualiza o email do usuário
            existente.HardDisk = maquinas.HardDisk;
            existente.Placa_Rede = maquinas.Placa_Rede;
            existente.Memoria_Ram = maquinas.Memoria_Ram;
            existente.Fk_Usuario = maquinas.Fk_Usuario;

            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return existente; // Retorna o usuário que foi atualizado

        }

        [HttpDelete("{id}")] // Define que esse método é um DELETE, {id} é um parâmetro que vai ser passado na URL
        public async Task<ActionResult> Delete(int id) // Task é um método assíncrono, ActionResult é o tipo de retorno do método
        {
            var existente = await _context.Maquinas.FindAsync(id); // Procura o usuário no banco de dados
            if (existente == null) return NotFound(); // Se não encontrar o usuário, retorna um erro 404
            _context.Maquinas.Remove(existente); // Remove o usuário do banco de dados
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna um status 204
        }
    }
}