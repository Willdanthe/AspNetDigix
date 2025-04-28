using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula05_SistemaEscolaAPI.DTO
{
    public class DisciplinaAlunoCursoDTO
    {
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public int CursoId { get; set; }
    }
}