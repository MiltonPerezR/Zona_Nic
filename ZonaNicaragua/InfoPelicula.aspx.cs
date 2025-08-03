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
    public partial class InfoPelicula : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();
        public static string sID = "-1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string userAgent = Request.UserAgent.ToLower();
                if (userAgent.Contains("android") || userAgent.Contains("smarttv") || userAgent.Contains("googletv") || userAgent.Contains("smart-tv"))
                {
                    btn2.Visible = false;
                }
                if (Request.QueryString["Id"] != null)
                {
                    sID = Request.QueryString["Id"].ToString();
                    MostrarDato();
                    peliculasRelacionadas();
                }
            }
        }
        public void peliculasRelacionadas()
        {
            int id = int.Parse(sID);

            var peli = Uow.Peliculas.FirstOrDefault(pl => pl.IdPelicula == id);

            var peliculasRelacionadas = Uow.Peliculas
                .Include(p => p.M_IMAGENH)
                .Select(p => new
                {
                    Id = p.IdPelicula,
                    p.TituloPelicula,
                    imagen = p.ImagenV.Select(iv => iv.UrlImagenV).FirstOrDefault() ?? "/imagenes/no-disponible.jpg",
                    p.FechaEstreno,
                    p.Calidad,
                    p.Genero
                })
                .Where(p => p.Id != peli.IdPelicula && p.Genero == peli.Genero)
                .OrderByDescending(p => p.Id)
                .Take(10)
                .OrderBy(x => Guid.NewGuid())
                .ToList();
            rptSugerencias.DataSource = peliculasRelacionadas;
            rptSugerencias.DataBind();
        }
        private void MostrarDato()
        {
            int id = int.Parse(sID);

            // Obtenemos la imagen y la película relacionada desde la base de datos
            var imagen = Uow.ImagenV
                            .Include(p => p.Peliculas)
                            .FirstOrDefault(p => p.IdPeliculaV == id);

            var imagen1 = Uow.M_IMAGENH
                            .Include(p => p.Peliculas)
                            .FirstOrDefault(p => p.IdPeliculaH == id);
            if (imagen1 != null && imagen1.Peliculas != null)
            {
                string userAgent = Request.UserAgent.ToLower();
                //if (Request.Browser.IsMobileDevice)
                //{
                    // Asignamos los valores a los controles
                    string urlFondo1 = imagen.UrlImagenV;
                    heroContainer.Attributes["style"] = $"background: url('{urlFondo1}') no-repeat center center; background-size: cover;";

                //}
                if (userAgent.Contains("smarttv") || userAgent.Contains("googletv") || userAgent.Contains("smart-tv"))
                {
                    // Asignamos los valores a los controles
                    string urlFondo = imagen1.UrlImagenH;

                    heroContainer.Attributes["style"] = $"background: url('{urlFondo}') no-repeat center center; background-size: cover;";

                }
                else if (userAgent.Contains("android"))
                {
                    // Asignamos los valores a los controles
                    string urlFondo = imagen.UrlImagenV;

                    heroContainer.Attributes["style"] = $"background: url('{urlFondo}') no-repeat center center; background-size: cover;";

                }
                else
                {
                    // Asignamos los valores a los controles
                    string urlFondo = imagen1.UrlImagenH;

                    heroContainer.Attributes["style"] = $"background: url('{urlFondo}') no-repeat center center; background-size: cover;";

                }


            }
            else
            {
                // Manejo de error si no se encuentra la película
                //lbltitulo.Text = "Película no encontrada";
            }
            lblTitulo.Text = imagen.Peliculas.TituloPelicula;
            lblSubtitulo.Text = imagen.Peliculas.Generos;
            lblDescripcion.Text = imagen.Peliculas.SinopsisPelicula;
        }
        protected void rptSugerencias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerPelicula")
            {
                string idPelicula = e.CommandArgument.ToString();

                // Aquí puedes redirigir a la página de esa película
                Response.Redirect("InfoPelicula.aspx?Id=" + idPelicula);
            }
        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            int idPelicula = int.Parse(sID.ToString());
            var pelicula = Uow.Peliculas.FirstOrDefault(p => p.IdPelicula == idPelicula);
            Response.Redirect(pelicula.UrlPelicula);
        }
        protected void btn2_Click(object sender, EventArgs e)
        {

            Response.Redirect($"Reproducir.aspx?Id={sID}&tipo=1");
        }
    }
}