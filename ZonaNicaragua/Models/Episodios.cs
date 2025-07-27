using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("M_EPISODIOS")]
    public class Episodios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEpisodio { get; set; }
        public string TituloEpisodio { get; set; }
        public string Descripcion { get; set; }
        public int NumeroEpisodio { get; set; }
        public string Miniatura { get; set; }
        public string UrlVideo { get; set; }
        public string TiempoEpisodio { get; set; }
        public int IdSerieE { get; set; }
        public int NumeroTemporada { get; set; }

        public virtual Series Series { get; set; }
        public int IdTemporadaE { get; set; }

        [ForeignKey("IdTemporadaE")]
        public virtual Temporadas Temporadas { get; set; }
    }
}
