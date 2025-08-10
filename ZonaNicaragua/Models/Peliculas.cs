using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ZonaNicaragua.Models
{
    
    [Table("M_PELICULA")]
    public class Peliculas
    {
        [Key]  // <- Esta es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para autoincremento
        public int IdPelicula { get; set; }

        public string UrlPelicula { get; set; }
        public string TituloPelicula { get; set; }
        public string FechaEstreno { get; set; }
        public string Calidad { get; set; }
        public string ClasificacionEdad { get; set; }
        public string SinopsisPelicula { get; set; }
        public string TiempoPelicula { get; set; }


        public string Genero { get; set; }
        public string Generos { get; set; }

        public ICollection<ImagenV> ImagenV { get; set; }
        public ICollection<M_IMAGENH> M_IMAGENH { get; set; }
    }

}