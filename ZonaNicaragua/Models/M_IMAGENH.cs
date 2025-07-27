using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("M_IMAGENH")]
    public class M_IMAGENH
    {
        [Key]  // <- Esta es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para autoincremento
        public int IdImagenH {  get; set; }
        public string UrlImagenH { get; set; }
        public bool EstadoImagenH { get; set; }
        public int IdPeliculaH { get; set; }

        [ForeignKey("IdPeliculaH")]
        public virtual Peliculas Peliculas { get; set; }
    }
}
