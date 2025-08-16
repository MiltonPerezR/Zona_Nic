using System;
using System.Linq;

namespace ZonaNicaragua
{
    public partial class Embe : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int videoId;
                int tipo;
                if (int.TryParse(Request.QueryString["id"], out videoId) && int.TryParse(Request.QueryString["tipo"], out tipo))
                {
                    MostrarDato(videoId, tipo);
                }
                else
                {
                    videoFrame.Attributes["src"] = "about:blank";
                }
            }
        }

        private void MostrarDato(int id, int tipo)
        {
            switch (tipo)
            {
                case 1: // Película
                    try
                    {
                        var pelicula = Uow.Peliculas.FirstOrDefault(p => p.IdPelicula == id);

                        if (pelicula != null)
                        {
                            // Asignar URL al iframe
                            videoFrame.Attributes["src"] = pelicula.UrlPelicula;
                        }
                        else
                        {
                            videoFrame.Attributes["src"] = "about:blank";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case 2: // Serie
                    try
                    {
                        var epi = Uow.Episodios.FirstOrDefault(p => p.IdEpisodio == id);

                        if (epi != null)
                        {
                            // Asignar URL al iframe
                            videoFrame.Attributes["src"] = epi.UrlVideo;
                        }
                        else
                        {
                            videoFrame.Attributes["src"] = "about:blank";
                        }

                    }
                    catch (Exception ex)
                    {
                        videoFrame.Attributes["src"] = "about:blank " +ex;
                    }
                    break;

                default:
                    videoFrame.Attributes["src"] = "about:blank";
                    break;
            }
        }
    }
}
