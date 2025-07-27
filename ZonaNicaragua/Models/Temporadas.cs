using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonaNicaragua.Models
{
    [Table("Temporadas")]
    public class Temporadas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTemporada { get; set; }
        public int NumeroTemporada { get; set; }
        public string NombreTemporada { get; set; }
        public int IdSerie { get; set; }

        public virtual Series Series { get; set; }
        public ICollection<Episodios> Episodios { get; set; }
    }
}
