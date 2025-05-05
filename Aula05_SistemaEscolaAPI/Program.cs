using Microsoft.EntityFrameworkCore;
using Aula05_SistemaEscolaAPI.Models;
using Aula05_SistemaEscolaAPI.DTO;
using Aula05_SistemaEscolaAPI.DB;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication;

try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

    // Adiciona serviçoes ao contêiner
    builder.Services.AddControllers(); // Adiciona suporte aos controladores
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => // Para gerar a documentação lá em baixo, que é o Scheaams
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema Escolar API", Version = "v1" });
    });

    //AddJwtBearer é o método que configura a autenticação JWT para o aplicativo AspNetCore. Ele permite que o aplicativo valide o KWT recebido nas solicitações HTTP.
    // JwtBearerDefaults é um sistema de autenticação padrão de tokens
    // AuthenticationScheme éum parâmetro que especifica o esquema de autenticação usado. Neste caso nosso estamos usando o JWT
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("minha-chave-é-ultra-segura-com-32-caracteres")), // Chave secreta para assinar o Token. Deve ser mantida em segredo
            ValidateIssuer = false, // Valida o emissor token()
            ValidateAudience = false
        };
    });


    var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection(); // Redireciona para HTTPS

    // Redirecionamento para o Frontend
    app.UseAuthentication(); // Ativa a autenticação, que valida os tokens jwt nas solicitações recebida. Isso garante apenas os usuários atenticados ter acesso API
    app.UseAuthorization(); // Habilta a Autorização

    // Para subir os arquivos estáticos
    app.UseStaticFiles(); // Permite que os arquivos estáticos seja utilizado para o cliente
    app.UseRouting(); // Rota qe permite que o ASP direcione as solicitaçãos para os controladores apropriados com base nas rotas definidas
    app.UseHttpsRedirection(); // Redirecionamento https.   

    app.MapGet("/", context =>
    {
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
    }
    );

    app.MapControllers(); // Mapeia os controllers
    app.Run();
}

catch (System.Exception ex)
{
    System.Console.WriteLine("Erro de: " + ex.Message);
}
