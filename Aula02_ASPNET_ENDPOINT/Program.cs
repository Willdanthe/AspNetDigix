using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula02_ASPNET_ENDPOINT.Controller;
// Instalando os pacotes AspNET com o comando: dotnet add package Microsoft.AspNetCore

// Vamos usar a ferramente Swagger para documentar a API, que já está incluida no ASP.NET COre, mas precisa de um pacote para funcionar. Para isso precisamos executar o comando: dotnet add package Swashbuckle.AspNetCore
using Microsoft.AspNetCore.Builder; // Isso serve para configurar a aplicação e interfaces. E classes para configurar a aplicação

using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.DependencyInjection; // Isso serve para configurar a aplicação e interface;


namespace Aula02_ASPNET_ENDPOINT
{
    public class Executar
    {
        static void Main(string[] args)
        {
            // vou chamar uma classe selead com o nome WebApplication  que repredenta uma aplicação web ASP.NET Core

            var builder = WebApplication.CreateBuilder(args); // Cria uma aplicação web. O args porque ele é uma array de string que pereseta os argumentos da linha de comando

            // Agora vou adicionar o werviços de controller do WebApplication

            builder.Services.AddControllers(); // Vai adicionar os serviõs de controller do webapplication

            builder.Services.AddEndpointsApiExplorer(); // Vai adicionar o Swagger para comeuntação da API

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection(); // Adiciona o middleware de redirecionamento HTTPS (Que é um protocolo de comunicação de segurança sobre uma rede de computadores), exemplo: http://localhost:5000 para http://localhost:5001

            app.UseAuthorization(); // Vai usar a autorização, adicionar o middleware de autorização que permite a a autorização do usuário

            app.MapControllers(); // Mapear os controllers, adiciona o middleware de roteamento que corresponde a solicitações HTTP a um controlador

            app.Run();

            // http://localhost:5000/swagger/index.html
        }
    }
}