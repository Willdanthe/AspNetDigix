using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula05_SistemaEscolaAPI.Models
{
    public class Disciplina
    {
        public int Id{ get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public Curso Curso { get; set; } = new ();
        
        public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; } = [];
        
    }
}