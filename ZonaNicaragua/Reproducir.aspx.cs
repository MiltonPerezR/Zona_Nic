using System;
using System.Linq;
using System.Web.UI;

namespace ZonaNicaragua
{
    public partial class Reproducir : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();
        protected string peliculaUrl;
        protected string peliculaTitulo;
        protected string peliculaImagen;
        public static string sID = "0";
        public static string sTipos = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null && Request.QueryString["tipo"] != null)
                {
                    sID = Request.QueryString["Id"].ToString();
                    sTipos = Request.QueryString["tipo"].ToString();
                    MostrarDato();
                }
            }
        }
        private void MostrarDato()
        {

            switch (sTipos)
            {
                case "1":
                    try
                    {
                        int id = int.Parse(sID);

                        var imagen = Uow.M_IMAGENH.FirstOrDefault(p => p.IdPeliculaH == id);
                        var peli = Uow.Peliculas.FirstOrDefault(p => p.IdPelicula == id);

                        if (imagen != null && peli != null)
                        {
                            peliculaUrl = peli.UrlPelicula;
                            peliculaTitulo = peli.TituloPelicula;
                            peliculaImagen = imagen.UrlImagenH;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case "2":
                    try
                    {
                        int id = int.Parse(sID);

                        var epi = Uow.Episodios.FirstOrDefault(p => p.IdEpisodio == id);

                        if (epi != null)
                        {
                            peliculaUrl = epi.UrlVideo;
                            peliculaTitulo = epi.TituloEpisodio;
                            peliculaImagen = epi.Miniatura;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        //lblResultado.Text = "Error: " + ex.Message;
                    }
                    break;

                default:

                    break;
            }
        }
    }
}