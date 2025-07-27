using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("M_IMAGENHS")]
    public class M_IMAGENHS
    {
        [Key]  // <- Esta es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para autoincremento
        public int IdImagenHS {  get; set; }
        public string UrlImagenHS { get; set; }
        public bool EstadoImagenHS { get; set; }
        public int IdSerieH { get; set; }

        [ForeignKey("IdSerieH")]
        public virtual Series Series { get; set; }
    }
}
