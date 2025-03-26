using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aula02_ASPNET_ENDPOINT_2.Models
{
    [Table("maquina")]
    public class Maquinas
    {
        [Column("id_maquina")]
        public int Id { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("velocidade")]
        public int Velocidade { get; set; }

        [Column("harddisk")]
        public int HardDisk { get; set; }

        [Column("placa_rede")]
        public int Placa_Rede { get; set; }

        [Column("memoria_ram")]
        public int Memoria_Ram { get; set; }
        
        [Column("fk_usuario")]
        [ForeignKey(nameof(Usuario))]
        public int Fk_Usuario { get; set; }
    }
}