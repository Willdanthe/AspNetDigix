using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula02_ASPNET_ENDPOINT_2.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Aula02_ASPNET_ENDPOINT_2
{
    public class Executar
    {
        static void Main(string[] args)
        {
            try
            {

                var builder = WebApplication.CreateBuilder(args);
                // COnfigurara a string de conexão com o banco de dados

                var conncetionString  = builder.Configuration.GetConnectionString("PostgresConnection"); // Pega a string de conexão do arquivo appserrings.json

                // Registrar o AppDbContext com o Postgres
                builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conncetionString));

                builder.Services.AddControllers();

                // Habilita o Swagger
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Chama o Swagger
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHttpsRedirection(); // Redireciona para HTTPS

                app.UseAuthorization(); // Habilta a Autorização

                app.MapControllers(); // Mapeia os controllers

                app.Run();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Erro de: " + ex.Message);
            }
        }
    }
}