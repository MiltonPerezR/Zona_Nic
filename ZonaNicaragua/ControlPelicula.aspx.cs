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
    public partial class ControlPelicula : System.Web.UI.Page
    {
        public AppDbContext Uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Uow = new AppDbContext();
                CargarUsuarios();
            }
        }
        private void CargarUsuarios()
        {
            var peliculas = Uow.Peliculas
                .OrderBy(u => u.TituloPelicula)
                .Include(u => u.Generos)
                .Select(u => new
                {
                    u.UrlPelicula,
                    u.IdPelicula,
                    u.TituloPelicula,
                    u.SinopsisPelicula,
                    u.Genero,
                    u.TiempoPelicula
                })
                .ToList();

            gvPelicula.DataSource = peliculas;
            gvPelicula.DataBind();

        }
        protected void gvPelicula_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idPelicula = Convert.ToInt32(e.CommandArgument);
            lblId.Text = idPelicula.ToString();

            Uow = new AppDbContext();
            var pelicula = Uow.Peliculas.FirstOrDefault(p => p.IdPelicula == idPelicula);

            var minia = Uow.ImagenV.FirstOrDefault(m => m.IdPeliculaV == pelicula.IdPelicula);
            var posterImagen = Uow.M_IMAGENH.FirstOrDefault(p => p.IdPeliculaH == idPelicula);

            switch (e.CommandName)
            {
                case "Visualizar":
                    lblTituloModal.Text = pelicula.TituloPelicula;
                    TituloTxt.Text = pelicula.TituloPelicula;
                    SinopsisTxt.Text = pelicula.SinopsisPelicula;
                    estrenoTxt.Text = pelicula.FechaEstreno;
                    GenerosTxt.Text = !string.IsNullOrEmpty(pelicula.Generos) ? pelicula.Generos : pelicula.Genero;
                    mini.ImageUrl = minia.UrlImagenV;
                    poster.ImageUrl = posterImagen.UrlImagenH;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "var myModal = new bootstrap.Modal(document.getElementById('modalVista')); myModal.show();", true);
                    break;

                case "Editar":
                    CargarDatos(); // Asumo que llena los campos del modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalEdit", "var modal = new bootstrap.Modal(document.getElementById('modalAcciones')); modal.show();", true);
                    break;

                case "Eliminar":
                    peliculaTxt.Text = pelicula.TituloPelicula;
                    lblId.Text = pelicula.IdPelicula.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalDelete", "var modal = new bootstrap.Modal(document.getElementById('mEliminar')); modal.show();", true);
                    break;
            }
        }

        private void campos()
        {
            if (string.IsNullOrEmpty(tburPelicula.Text))
            {
                lblConfirmacion.Text = "El campo de url de película esta vacío";
                return;
            }
            if (string.IsNullOrEmpty(txttTitulo.Text))
            {
                lblConfirmacion.Text = "El campo de titulo de la película esta vacío";
                return;
            }
            if (Genero.Text == "")
            {
                lblConfirmacion.Text = "Debes de marcar un genero";
                return;
            }
            if (string.IsNullOrEmpty(tbDuracion.Text))
            {
                lblConfirmacion.Text = "El campo de duración esta vacío";
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
            var peliculas = Uow.Peliculas
                .FirstOrDefault(u => u.IdPelicula == lb);
            var imagenV = Uow.ImagenV
                .FirstOrDefault(u => u.IdPeliculaV == peliculas.IdPelicula);
            var imagenH = Uow.M_IMAGENH
                .FirstOrDefault(u => u.IdPeliculaH == peliculas.IdPelicula);


            txttTitulo.Text = peliculas.TituloPelicula;
            txtSinopsis.Text = peliculas.SinopsisPelicula;
            tbDuracion.Text = peliculas.TiempoPelicula;
            calidad.Text = peliculas.Calidad;
            tburPelicula.Text = peliculas.UrlPelicula;
            tbImagenV.Text = imagenV.UrlImagenV;
            tbImagenH.Text = imagenH.UrlImagenH;
            previewV.ImageUrl = imagenV.UrlImagenV;
            previewH.ImageUrl = imagenH.UrlImagenH;
            Genero.Text = peliculas.Genero;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();

            int lb = int.Parse(lblId.Text);
            campos();

            var addPelicula = Uow.Peliculas.FirstOrDefault(p => p.IdPelicula == lb);
            addPelicula.UrlPelicula = tburPelicula.Text;
            addPelicula.TituloPelicula = txttTitulo.Text;
            addPelicula.SinopsisPelicula = txtSinopsis.Text;
            addPelicula.TiempoPelicula = tbDuracion.Text;
            addPelicula.Calidad = calidad.Text;
            addPelicula.Genero = Genero.Text;
            Uow.SaveChanges();

            var addImagenV = Uow.ImagenV.FirstOrDefault(v => v.IdPeliculaV == lb);
            addImagenV.UrlImagenV = tbImagenV.Text;
            Uow.SaveChanges();

            var addImagenH = Uow.M_IMAGENH.FirstOrDefault(h => h.IdPeliculaH == lb);
            addImagenH.UrlImagenH = tbImagenH.Text;
            Uow.SaveChanges();

            // Mostrar alerta
            string script = $"mostrarAlerta('Bien', 'Registro guardado en la base de datos.', 'success');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

            // Actualizar tabla
            var peliculas = Uow.Peliculas
                .OrderBy(u => u.TituloPelicula)
                .Include(u => u.Generos)
                .Select(u => new
                {
                    u.UrlPelicula,
                    u.IdPelicula,
                    u.TituloPelicula,
                    u.SinopsisPelicula,
                    u.Genero,
                    u.TiempoPelicula
                })
                .ToList();

            gvPelicula.DataSource = peliculas;
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

                var iV = Uow.ImagenV.FirstOrDefault(i => i.IdPeliculaV == id);
                var iH = Uow.M_IMAGENH.FirstOrDefault(i => i.IdPeliculaH == id);
                var pelicula = Uow.Peliculas.FirstOrDefault(p => p.IdPelicula == id);

                if (pelicula != null)
                {
                    Uow.ImagenV.Remove(iV);
                    Uow.SaveChanges();

                    Uow.M_IMAGENH.Remove(iH);
                    Uow.SaveChanges();

                    Uow.Peliculas.Remove(pelicula);
                    Uow.SaveChanges();


                    string script = $"mostrarAlerta('Bien', 'Película Eliminada.', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    var peliculas = Uow.Peliculas
                .OrderBy(u => u.TituloPelicula)
                .Include(u => u.Generos)
                .Select(u => new
                {
                    u.UrlPelicula,
                    u.IdPelicula,
                    u.TituloPelicula,
                    u.SinopsisPelicula,
                    u.Genero,
                    u.TiempoPelicula
                })
                .ToList();

                    gvPelicula.DataSource = peliculas;
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