using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ZonaNicaragua.Models
{

    [Table("M_USUARIO")]
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public string Usuario { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }

        public int IdTipoUsuario { get; set; }


        [ForeignKey("IdTipoUsuario")]
        public virtual TipoUsuarios TipoUsuarios { get; set; }

    }

}