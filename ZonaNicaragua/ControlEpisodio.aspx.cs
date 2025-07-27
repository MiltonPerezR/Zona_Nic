using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZonaNicaragua.Models;

namespace ZonaNicaragua
{
    public partial class ControlEpisodio : System.Web.UI.Page
    {
        public AppDbContext Uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Uow = new AppDbContext();
                CargarSeries();
            }
        }
        private void CargarSeries()
        {
            var episodio = Uow.Episodios
                .OrderBy(u => u.TituloEpisodio)
                .Include(u => u.Series)
                .Include(u => u.Temporadas)
                .Select(u => new
                {
                    u.IdEpisodio,
                    u.TituloEpisodio,
                    u.Descripcion,
                    u.UrlVideo,
                    u.NumeroEpisodio,
                    u.Temporadas.NombreTemporada,
                    u.TiempoEpisodio,
                    u.Series.TituloSerie
                })
                .ToList();

            gvPelicula.DataSource = episodio;
            gvPelicula.DataBind();

        }
        protected void gvPelicula_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idEpi = Convert.ToInt32(e.CommandArgument);

            lblId.Text = idEpi.ToString();

            Uow = new AppDbContext();
            switch (e.CommandName)
            {
                case "Visualizar":
                    var v = Uow.Episodios.FirstOrDefault(v1 => v1.IdEpisodio == idEpi);
                    Label1.Text = v.TituloEpisodio;
                    TituloTxt.Text = v.TituloEpisodio;
                    Descripción.Text = v.Descripcion;
                    duracionTxt.Text = v.TiempoEpisodio;
                    Image1.ImageUrl = v.Miniatura;
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#modalVista').modal('show');", true);
                    break;

                case "Editar":
                    mostarDatos(idEpi);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#modalAcciones').modal('show');", true);
                    break;

                case "Eliminar":
                    Uow = new AppDbContext();
                    var epi = Uow.Episodios.FirstOrDefault(p => p.IdEpisodio == idEpi);
                    peliculaTxt.Text = epi.TituloEpisodio;
                    lblId.Text = epi.IdEpisodio.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#mEliminar').modal('show');", true);
                    break;
            }
        }
        private void mostarDatos(int idSerieGuardada)
        {
            var series = Uow.Series.ToList();

            // Reordenar: primero la serie guardada
            var serieSeleccionada = Uow.Episodios.FirstOrDefault(s => s.IdEpisodio == idSerieGuardada);
            var serieS = Uow.Series.FirstOrDefault(e => e.IdSerie == serieSeleccionada.IdSerieE);

            txttTitulo.Text = serieSeleccionada.TituloEpisodio;
            txtSinopsis.Text = serieSeleccionada.Descripcion;
            tbImagenH.Text = serieSeleccionada.Miniatura;
            Image2.ImageUrl = serieSeleccionada.Miniatura;
            tnTiempoEpi.Text = serieSeleccionada.TiempoEpisodio;
            var tem = Uow.Temporadas.FirstOrDefault(t => t.IdTemporada == serieSeleccionada.IdTemporadaE);
            lblUrl.Text = serieSeleccionada.UrlVideo;
        }

        protected void lbUpdate_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();
            int idEpi = int.Parse(lblId.Text);
            var addEpisodio = Uow.Episodios.FirstOrDefault(epi => epi.IdEpisodio == idEpi);
            if (addEpisodio != null)
            {
                addEpisodio.TituloEpisodio = txttTitulo.Text;
                addEpisodio.Descripcion = txtSinopsis.Text;
                addEpisodio.Miniatura = tbImagenH.Text;
                addEpisodio.UrlVideo = lblUrl.Text;
                addEpisodio.TiempoEpisodio = tnTiempoEpi.Text;
                Uow.SaveChanges();
                string script = $"mostrarAlerta('Bien', 'Registro guardado en la base de datos.', 'success');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Uow = new AppDbContext();
                int id = int.Parse(lblId.Text);

                var epi = Uow.Episodios.FirstOrDefault(p => p.IdEpisodio == id);

                if (epi != null)
                {
                    Uow.Episodios.Remove(epi);
                    Uow.SaveChanges();

                    string script = $"mostrarAlerta('Bien', 'Episodio Eliminada.', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                }
                else
                {
                    string script = $"mostrarAlerta('Error', 'No se encontró la película.', 'error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
            catch (Exception)
            {
                string script = $"mostrarAlerta('Error', 'Error en la eliminación.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }
    }
}