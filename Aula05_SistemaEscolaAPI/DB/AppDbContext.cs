using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aula05_SistemaEscolaAPI.Models;

namespace Aula05_SistemaEscolaAPI.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<DisciplinaAlunoCurso> DisciplinasAlunosCursos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DisciplinaAlunoCurso>()
            .HasKey(dc => new {dc.AlunoId, dc.CursoId, dc.DisciplinaId});

            modelBuilder.Entity<Aluno>()
            .HasKey(dc => new {dc.Id});

            modelBuilder.Entity<Disciplina>()
            .HasKey(dc => new {dc.Id});

            modelBuilder.Entity<Curso>()
            .HasKey(dc => new {dc.Id});

        }

    }
}