using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("M_IMAGENVS")]
    public class M_IMAGENVS
    {
        [Key]  // <- Esta es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para autoincremento
        public int IdImagenVS {  get; set; }
        public string UrlImagenVS { get; set; }
        public bool EstadoImagenVS { get; set; }
        public int IdSerieV { get; set; }

        [ForeignKey("IdSerieV")]
        public virtual Series Series { get; set; }
    }
}
