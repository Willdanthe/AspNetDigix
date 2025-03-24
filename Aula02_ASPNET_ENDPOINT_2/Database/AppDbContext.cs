using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aula02_ASPNET_ENDPOINT_2.Models;

// precisa isntalar:
// dotnet add package Microsoft.EntityFrameworkCore;
// dotnet add package Microsoft.EntityFrameworkCore.Design;
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL;
//

namespace Aula02_ASPNET_ENDPOINT_2.Database
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Models.Usuario> Usuarios { get; set; }

    }
}