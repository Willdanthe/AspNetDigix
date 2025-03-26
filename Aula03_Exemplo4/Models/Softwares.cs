using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aula03_Exemplo4.Models
{
    [Table("software")]
    public class Softwares
    {
        [Column("id_software")]
        public int Id { get; set; }

        [Column("produto")]
        public string Produto { get; set; }

        [Column("harddisk")]
        public int HardDisk { get; set; }

        [Column("memoria_ram")]
        public int Memoria_Ram { get; set; }
        
        [Column("fk_maquina")]
        [ForeignKey(nameof(Maquinas))]
        public int Fk_Maquina { get; set; }
    }
}