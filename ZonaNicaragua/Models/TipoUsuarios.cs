using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZonaNicaragua.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("M_TIPO_USUARIO")]
    public class TipoUsuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoUsuario { get; set; }

        public string TipoUsuario { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }

    }
}