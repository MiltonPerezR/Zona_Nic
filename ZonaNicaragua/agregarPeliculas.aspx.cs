using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ZonaNicaragua.Models; // Asegúrate que tu modelo esté en este namespace

namespace ZonaNicaragua
{
    public partial class agregarPeliculas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected async void btnBuscar_Click(object sender, EventArgs e)
        {
            string titulo = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(titulo))
            {
                var datos = await ObtenerInformacionPelicula(titulo);

                if (datos != null)
                {
                    lblTitulo.Text = datos["title"]?.ToString();
                    lblSinopsis.Text = datos["overview"]?.ToString();
                    lblEstreno.Text = datos["release_date"]?.ToString();
                    lblPuntuacion.Text = datos["vote_average"]?.ToString();

                    var generos = datos["genres"];
                    if (generos != null && generos.HasValues)
                        lblGenero.Text = generos[0]["name"]?.ToString();

                    string posterPath = datos["poster_path"]?.ToString();
                    if (!string.IsNullOrEmpty(posterPath))
                        imgPoster.ImageUrl = $"https://image.tmdb.org/t/p/w500{posterPath}";

                    // Guardar en hiddenfields
                    hfTitulo.Value = lblTitulo.Text;
                    hfSinopsis.Value = lblSinopsis.Text;
                    hfEstreno.Value = lblEstreno.Text;
                    hfPuntuacion.Value = lblPuntuacion.Text;
                    hfGenero.Value = lblGenero.Text;
                    hfPoster.Value = imgPoster.ImageUrl;

                    pnlResultado.Visible = true;
                }
                else
                {
                    pnlResultado.Visible = false;
                }
            }
        }

        private async Task<JObject> ObtenerInformacionPelicula(string titulo)
        {
            string apiKey = "1bc6db497c4e844d1abec56c8808a145"; // ← Reemplaza con tu clave real de TMDb
            string urlBusqueda = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&language=es-ES&query={titulo}";

            using (HttpClient client = new HttpClient())
            {
                var respuesta = await client.GetStringAsync(urlBusqueda);
                JObject json = JObject.Parse(respuesta);

                var resultados = json["results"];
                if (resultados != null && resultados.HasValues)
                {
                    var idPelicula = resultados[0]["id"];
                    string urlDetalles = $"https://api.themoviedb.org/3/movie/{idPelicula}?api_key={apiKey}&language=es-ES";

                    var detalle = await client.GetStringAsync(urlDetalles);
                    return JObject.Parse(detalle);
                }
            }

            return null;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
        //    using (var db = new AppDbContext())
        //    {
        //        var pelicula = new Pelicula
        //        {
        //            TituloPelicula = hfTitulo.Value,
        //            Sinopsis = hfSinopsis.Value,
        //            FechaEstreno = DateTime.TryParse(hfEstreno.Value, out var fecha) ? fecha : DateTime.Now,
        //            GeneroPrincipal = hfGenero.Value,
        //            Puntaje = decimal.TryParse(hfPuntuacion.Value, out var puntuacion) ? puntuacion : 0,
        //            UrlImagenPelicula = hfPoster.Value
        //        };

        //        db.Peliculas.Add(pelicula);
        //        db.SaveChanges();
        //    }

        //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Película guardada correctamente.');", true);
        }
    }
}
