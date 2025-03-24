using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula02_ASPNET_ENDPOINT.Models;


// Para a genre poder fazer os protocolos HTP, precisamos de um controlelr
// da biblioteca do ASP.NET, que é o MVC no qual lá tem o ControllerBase
using Microsoft.AspNetCore.Mvc;
//--> dotnet add package Microsoft.AspNetCore.Mvc <--
    
namespace Aula02_ASPNET_ENDPOINT.Controller
{
    // Vamos adicioanr um anotattion para dizer que essa classe é um controller para refereniar os endpoints e metodos HTTP
    [ApiController]
    // Vamos adicionar outro Anotaion para dizer que essa classe é um controller para referenciar os endpoints e metodos HTTP, definindo a rota para acessar esse controller
    [Route("[controller]")]

    public class UsuarioController : ControllerBase
    {
        private static List<Usuario> Usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "Will", Email = "will@gmail.com"},
            new Usuario { Id = 2, Nome = "Gravena", Email = "gravena@gmail.com"},
            new Usuario { Id = 3, Nome = "Arthut", Email = "arthur@gmail.com"}
        };

        // Vamo chamar o Anotation para definir a rota para acessar esse método de requisição
        [HttpGet]
        public IEnumerable<Usuario> Get() // IEnumerable é ua interface que representa uma coleção de objetos que podem ser iterados
        {
            return Usuarios; // Vai retornar a lista de Usuários
        }

        // Anotation para definir a rota para acessar esse método de requisição HTTP de adicionar um usuário
        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario) // FromBody é um atributo que indica que um parâmetro da ação deve ser ligado ao corpo da solicitação HTTP. O atributo FromBody informa ao MVC para ler o valor do corpo da solicitação HTTP e ligá-lo ao parâmetro do método
        {
            Usuarios.Add(usuario);
            return usuario;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            var UsuarioExiste = Usuarios.Where(x => x.Id == id).FirstOrDefault();
            
            if(UsuarioExiste != null)
            {
                UsuarioExiste.Nome = usuario.Nome;
                UsuarioExiste.Email = usuario.Email;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var UsuarioExiste = Usuarios.Where(x => x.Id == id).FirstOrDefault();

            if (UsuarioExiste != null)
            {
                Usuarios.Remove(UsuarioExiste);
            }
        }
    }
}