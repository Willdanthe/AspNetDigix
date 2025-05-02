using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aula05_SistemaEscolaAPI.DTO
{
    public class DisciplinaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Curso { get; set; } = string.Empty;
        
    }
}