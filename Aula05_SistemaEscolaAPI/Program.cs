using Microsoft.EntityFrameworkCore;
using Aula05_SistemaEscolaAPI.Models;
using Aula05_SistemaEscolaAPI.DTO;
using Aula05_SistemaEscolaAPI.DB;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;

try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

    builder.Services.AddControllers(); // Adiciona suporte aos controladores
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen( c => // Para gerar a documentação lá em baixo, que é o Scheaams
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema Escolar API", Version = "v1"});
    });

    var app = builder.Build();

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
