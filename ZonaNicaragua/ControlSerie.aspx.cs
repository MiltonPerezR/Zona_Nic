using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZonaNicaragua
{
    public partial class ControlSerie : System.Web.UI.Page
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
            var series = Uow.Series
                .OrderBy(u => u.TituloSerie)
                .Select(u => new
                {
                    u.IdSerie,
                    u.TituloSerie,
                    u.SinopsisSerie,
                    u.Genero,
                })
                .ToList();

            gvPelicula.DataSource = series;
            gvPelicula.DataBind();

        }
        protected void gvPelicula_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idSerie = Convert.ToInt32(e.CommandArgument);

            lblId.Text = idSerie.ToString();

            Uow = new AppDbContext();

            var serie = Uow.Series
                .FirstOrDefault(s => s.IdSerie == idSerie);

            var minia = Uow.M_IMAGENVS.FirstOrDefault(m => m.IdSerieV == serie.IdSerie);

            var poster = Uow.M_IMAGENHS.FirstOrDefault(p => p.IdSerieH == idSerie);

            switch (e.CommandName)
            {
                case "Visualizar":
                    var series = Uow.Series.FirstOrDefault(s => s.IdSerie == idSerie);
                    Label1.Text = series.TituloSerie;
                    TituloTxt.Text = series.TituloSerie;
                    SinopsisTxt.Text = series.SinopsisSerie;
                    generot.Text = series.Genero;
                    mini.ImageUrl = minia.UrlImagenVS;
                    pos.ImageUrl = poster.UrlImagenHS;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#modalVista').modal('show');", true);
                    break;

                case "Editar":
                    CargarDatos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#modalAcciones').modal('show');", true);
                    break;

                case "Eliminar":
                    peliculaTxt.Text = serie.TituloSerie;
                    lblId.Text = serie.IdSerie.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalDelete", "var modal = new bootstrap.Modal(document.getElementById('mEliminar')); modal.show();", true);
                    break;
            }
        }
        private void campos()
        {
            if (string.IsNullOrEmpty(txttTitulo.Text))
            {
                lblConfirmacion.Text = "El campo de titulo de la película esta vacío";
                return;
            }
            if (string.IsNullOrEmpty(tbImagenV.Text))
            {
                lblConfirmacion.Text = "El campo de imagen vertical esta vacío";
                return;
            }
        }
        private void CargarDatos()
        {
            int lb = int.Parse(lblId.Text);
            var series = Uow.Series
                .FirstOrDefault(u => u.IdSerie == lb);
            var imagenHS = Uow.M_IMAGENHS
                .FirstOrDefault(u => u.IdSerieH == series.IdSerie);
            var imagenVS = Uow.M_IMAGENVS
                .FirstOrDefault(u => u.IdSerieV == series.IdSerie);

            txttTitulo.Text = series.TituloSerie;
            txtSinopsis.Text = series.SinopsisSerie;
            tbImagenH.Text = imagenHS.UrlImagenHS;
            tbImagenV.Text = imagenVS.UrlImagenVS;
            genero.Text = series.Genero;
            generos.Text = series.Generos;
            previewH.ImageUrl = imagenHS.UrlImagenHS;
            previewV.ImageUrl = imagenVS.UrlImagenVS;
            fechaEstreno.Text = series.FechaEstreno;
            clasificacionEdad.Text = $"+{series.ClasificacionEdad}";
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();

            int lb = int.Parse(lblId.Text);
            campos();

            var s = Uow.Series.FirstOrDefault(p => p.IdSerie == lb);
            s.TituloSerie = txttTitulo.Text;
            s.SinopsisSerie = txtSinopsis.Text;
            s.Genero = genero.Text;
            s.Generos = generos.Text;
            s.FechaEstreno = fechaEstreno.Text;
            s.ClasificacionEdad = clasificacionEdad.Text;
            Uow.SaveChanges();

            var addImagenV = Uow.M_IMAGENVS.FirstOrDefault(v => v.IdSerieV == lb);
            addImagenV.UrlImagenVS = tbImagenV.Text;
            Uow.SaveChanges();

            var addImagenHS = Uow.M_IMAGENHS.FirstOrDefault(h => h.IdSerieH == lb);
            addImagenHS.UrlImagenHS = tbImagenH.Text;
            Uow.SaveChanges();

            string script = $"mostrarAlerta('Bien', 'Registro guardado en la base de datos.', 'success');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

            var series = Uow.Series
                .OrderBy(u => u.TituloSerie)
                .Select(u => new
                {
                    u.IdSerie,
                    u.TituloSerie,
                    u.SinopsisSerie,
                    u.Genero,
                })
                .ToList();

            gvPelicula.DataSource = series;
            gvPelicula.DataBind();

            // Cerrar modal y eliminar backdrop
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarModal", "$('#modalAcciones').modal('hide');", true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            try
            {
                Uow = new AppDbContext();
                int id = int.Parse(lblId.Text);

                var iV = Uow.M_IMAGENVS.FirstOrDefault(i => i.IdSerieV == id);
                var iH = Uow.M_IMAGENHS.FirstOrDefault(i => i.IdSerieH == id);
                var Series = Uow.Series.FirstOrDefault(p => p.IdSerie == id);

                if (Series != null)
                {
                    Uow.M_IMAGENVS.Remove(iV);
                    Uow.SaveChanges();

                    Uow.M_IMAGENHS.Remove(iH);
                    Uow.SaveChanges();

                    Uow.Series.Remove(Series);
                    Uow.SaveChanges();


                    string script = $"mostrarAlerta('Bien', 'Serie Eliminada.', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    var series = Uow.Series
                .OrderBy(u => u.TituloSerie)
                .Select(u => new
                {
                    u.IdSerie,
                    u.TituloSerie,
                    u.SinopsisSerie,
                    u.Genero,
                })
                .ToList();

                    gvPelicula.DataSource = series;
                    gvPelicula.DataBind();
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