using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("M_SERIE")]
    public class Series
    {
        [Key]  // <- Esta es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para autoincremento
        public int IdSerie { get; set; }
        public string TituloSerie { get; set; }
        public string SinopsisSerie { get; set; }
        public int IdTipoVideo { get; set; }
        public string Generos { get; set; }
        public string FechaEstreno { get; set; }
        public string Genero { get; set; }
        public int IdEpisodioQuedo { get; set; }


        public virtual TipoVideos TipoVideos { get; set; }
        public ICollection<M_IMAGENHS> M_IMAGENHS { get; set; }
        public ICollection<Episodios> Episodios { get; set; }
        public ICollection<M_IMAGENVS> M_IMAGENVS { get; set; }
        public ICollection<Temporadas> Temporadas { get; set; }
    }
}
