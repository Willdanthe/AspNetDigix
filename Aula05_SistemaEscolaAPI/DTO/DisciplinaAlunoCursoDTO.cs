using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula05_SistemaEscolaAPI.Models;

namespace Aula05_SistemaEscolaAPI.DTO
{
    public class DisciplinaAlunoCursoDTO
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public int CursoId { get; set; }

        public string AlunoNome { get; set; } = string.Empty;
        public string CursoDescricao { get; set; } = string.Empty;
        public string DisciplinaDescricao { get; set; } = string.Empty;

    }
}