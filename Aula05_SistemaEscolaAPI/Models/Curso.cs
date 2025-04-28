using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula05_SistemaEscolaAPI.Models
{
    public class Curso
    {
        public int Id{ get; set; }
        public string Descricao { get; set; } = string.Empty;

        //De acordo com o diagrama inicial, isso não é recomendadao, porém eu 
        // public ICollection<Aluno> Alunos { get; set; } = [];
        public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; } = [];
        
        
    }
}