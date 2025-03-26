using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aula03_ASPNET_API_ADO.Models;

// precisa isntalar:
// dotnet add package Microsoft.EntityFrameworkCore;
// dotnet add package Microsoft.EntityFrameworkCore.Design;
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL;
//

namespace Aula03_ASPNET_API_ADO.Database
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Maquinas> Maquinas { get; set; }
        public DbSet<Softwares> Softwares { get; set; }

    }
}