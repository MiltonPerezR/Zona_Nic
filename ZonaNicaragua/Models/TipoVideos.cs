using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("M_TIPO_VIDEO")]
    public class TipoVideos
    {
        [Key]  // <- Esta es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para autoincremento
        public int IdTipoVideo { get; set; }
        public string TipoVideo { get; set; }
        public ICollection<Series> Series { get; set; }
    }
}
