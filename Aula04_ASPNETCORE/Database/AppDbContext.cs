using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aula04_ASPNETCORE.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Usuario> Usuarios { get; set; }
        public DbSet<Models.Maquinas> Maquinas { get; set; }
        public DbSet<Models.Softwares> Softwares { get; set; }
    }
}