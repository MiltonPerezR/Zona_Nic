using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZonaNicaragua
{
    public partial class Control : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            lblCantidadSeries.Text = Uow.Series
                .Where(u => u.IdTipoVideo == 1)
                .ToList().Count.ToString();

            lblCantidadesAnimes.Text = Uow.Series
                .Where(u => u.IdTipoVideo == 2)
                .ToList().Count.ToString();

            lblcountPeliculas.Text = Uow.Peliculas.ToList().Count.ToString();
            lblCantidadUsuarios.Text = Uow.Usuarios.ToList().Count.ToString();

            lblCantidadEpisodios.Text = Uow.Episodios.ToList().Count.ToString();
        }
    }
}