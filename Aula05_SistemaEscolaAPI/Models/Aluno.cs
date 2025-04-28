using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula05_SistemaEscolaAPI.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public Curso Curso { get; set; } = new Curso();

        // Icollection é uma coleção que pode ser sada para armazenar uma lista de objetos, de forma que possa ser manipulada
        // Assim é a coleção de Disciplinas que o aluno está matrcilado
        public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; } = [];
    }
}