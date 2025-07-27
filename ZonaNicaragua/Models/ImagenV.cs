using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("M_IMAGENV")]
    public class ImagenV
    {
        [Key]  // <- Esta es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para autoincremento
        public int IdImagenV {  get; set; }
        public string UrlImagenV { get; set; }
        public bool EstadoImagenV { get; set; }
        public int IdPeliculaV { get; set; }

        [ForeignKey("IdPeliculaV")]
        public virtual Peliculas Peliculas { get; set; }
    }
}
