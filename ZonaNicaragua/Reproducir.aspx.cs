using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace ZonaNicaragua
{
    public partial class Reproducir : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();
        protected string peliculaUrl;
        protected string peliculaTitulo;
        protected string peliculaImagen;
        protected string tipo;
        public static string sID = "0";
        public static string sTipos = "";

        protected string playlistJson;
        protected string recomendacionesJson;

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
                case "1": // Película
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
                        tipo = "Pelicula";

                        // Traer películas a memoria para evitar error con string.Format en LINQ to Entities
                        var peliculasSinUrl = Uow.Peliculas
                            .Where(p => p.IdPelicula != id)
                            .ToList();

                        var recomendaciones = peliculasSinUrl
                            .Select(p => new
                            {
                                title = p.TituloPelicula,
                                poster = Uow.M_IMAGENH.FirstOrDefault(i => i.IdPeliculaH == p.IdPelicula)?.UrlImagenH,
                                link = $"Reproducir.aspx?Id={p.IdPelicula}&tipo=1"
                            })
                            .Take(10)
                            .ToList();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        recomendacionesJson = js.Serialize(recomendaciones);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case "2": // Serie
                    try
                    {
                        int id = int.Parse(sID);

                        var epi = Uow.Episodios.FirstOrDefault(p => p.IdEpisodio == id);

                        var playlistRaw = Uow.Episodios
                        .Where(p => p.IdSerieE == epi.IdSerieE)
                        .OrderBy(p => p.NumeroTemporada)
                        .ThenBy(p => p.NumeroEpisodio)
                        .Select(p => new
                        {
                            p.NumeroTemporada,
                            p.NumeroEpisodio,
                            p.TituloEpisodio,
                            p.UrlVideo,
                            p.Miniatura
                        })
                        .ToList(); // Aquí EF ejecuta la consulta y trae a memoria

                        var playlist = playlistRaw
                            .Select(p => new
                            {
                                title = $"T{p.NumeroTemporada}E{p.NumeroEpisodio} - {p.TituloEpisodio}",
                                file = p.UrlVideo,
                                poster = p.Miniatura,
                                temporada = p.NumeroTemporada,
                                episodio = p.NumeroEpisodio
                            })
                            .ToList();


                        tipo = "Serie";

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        playlistJson = js.Serialize(playlist);

                        // Traer episodios a memoria para formar links con string interpolation
                        var episodiosSerie = Uow.Episodios
                            .Where(p => p.IdSerieE == epi.IdSerieE && p.IdEpisodio != id)
                            .OrderBy(p => p.NumeroEpisodio)
                            .ToList();

                        var recomendaciones = episodiosSerie
                            .Select(p => new
                            {
                                title = p.TituloEpisodio,
                                poster = p.Miniatura,
                                link = $"Reproducir.aspx?Id={p.IdEpisodio}&tipo=2"
                            })
                            .Take(10)
                            .ToList();

                        recomendacionesJson = js.Serialize(recomendaciones);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
