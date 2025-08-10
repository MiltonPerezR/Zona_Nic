using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZonaNicaragua.Models;

namespace ZonaNicaragua
{
    public partial class addPeliculas : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var peliculaExiste = Uow.Peliculas
                .Where(p => p.UrlPelicula == tburPelicula.Text || p.TituloPelicula == txttTitulo.Text)
                .ToList();

            if (peliculaExiste.Count == 0)
            {
                var peliculaExiste1 = Uow.Peliculas
                .Where(p => p.TituloPelicula == txttTitulo.Text)
                .ToList();
                if (peliculaExiste1.Count == 0)
                {
                    var addPelicula = new Peliculas
                    {
                        UrlPelicula = tburPelicula.Text,
                        TituloPelicula = txttTitulo.Text,
                        SinopsisPelicula = txtSinopsis.Text,
                        TiempoPelicula = tbDuracion.Text,
                        Genero = genero.Text,
                        Generos = generosM.Text,
                        Calidad = Calidad.Text,
                        FechaEstreno = fechaEstreno.Text,
                        ClasificacionEdad = txtClasificacionEdad.Text,
                    };
                    Uow.Peliculas.Add(addPelicula);
                    Uow.SaveChanges();

                    var addImagenV = new ImagenV
                    {
                        UrlImagenV = tbImagenH.Text,
                        EstadoImagenV = true,
                        IdPeliculaV = addPelicula.IdPelicula
                    };
                    Uow.ImagenV.Add(addImagenV);
                    Uow.SaveChanges();

                    var addImagenH = new M_IMAGENH
                    {
                        UrlImagenH = tbImagenV.Text,
                        EstadoImagenH = true,
                        IdPeliculaH = addPelicula.IdPelicula
                    };
                    Uow.M_IMAGENH.Add(addImagenH);
                    Uow.SaveChanges();

                    resetCampos();

                    string script = $"mostrarAlerta('Bien', 'Registro guardado en la base de datos.', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
                else
                {
                    string script = $"mostrarAlerta('Error', 'Este título ya existe en la base de datos.', 'error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
            else
            {
                string script = $"mostrarAlerta('Error', 'La url de pelicula ya existe en la base de datos.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }

        private void resetCampos()
        {
            tburPelicula.Text = string.Empty;
            txttTitulo.Text = string.Empty;
            txtSinopsis.Text = string.Empty;
            tbDuracion.Text = string.Empty;
            tbImagenV.Text = string.Empty;
            tbImagenH.Text = string.Empty;
            genero.Text = string.Empty;
            generosM.Text = string.Empty;
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlPelicula.aspx");
        }

        protected async void btnBuscar_Click(object sender, EventArgs e)
        {
            string titulo = txttTitulo.Text.Trim();
            if (!string.IsNullOrEmpty(titulo))
            {
                var datos = await ObtenerInformacionPelicula(titulo);

                if (datos != null)
                {
                    // Título, sinopsis, fecha
                    txttTitulo.Text = datos["title"]?.ToString();
                    txtSinopsis.Text = datos["overview"]?.ToString();
                    fechaEstreno.Text = datos["release_date"]?.ToString();

                    // Géneros
                    var generos = datos["genres"];
                    if (generos != null && generos.HasValues)
                    {
                        string generoTMDb = generos[0]["name"]?.ToString();
                        genero.Text = generoTMDb;

                        var nombresGeneros = generos.Select(g => g["name"]?.ToString()).ToList();
                        generosM.Text = string.Join(", ", nombresGeneros);
                    }

                    // Imagen vertical (poster)
                    string posterPath = datos["poster_path"]?.ToString();
                    if (!string.IsNullOrEmpty(posterPath))
                    {
                        string posterUrl = $"https://image.tmdb.org/t/p/w500{posterPath}";
                        tbImagenH.Text = posterUrl;
                        imgPosterH.ImageUrl = posterUrl;
                    }

                    // Imagen horizontal (backdrop)
                    string backdropPath = datos["backdrop_path"]?.ToString();
                    if (!string.IsNullOrEmpty(backdropPath))
                    {
                        string backdropUrl = $"https://image.tmdb.org/t/p/w780{backdropPath}";
                        imgPoster.ImageUrl = backdropUrl;
                        tbImagenV.Text = backdropUrl;
                    }

                    // Duración
                    int duracion = datos["runtime"]?.ToObject<int>() ?? 0;
                    if (duracion > 0)
                    {
                        int horas = duracion / 60;
                        int minutos = duracion % 60;
                        tbDuracion.Text = $"{horas}h {minutos}m";
                    }
                    else
                    {
                        tbDuracion.Text = "Duración no disponible";
                    }

                    // Clasificación de edad
                    var certificaciones = datos["certification"]?["results"];
                    if (certificaciones != null)
                    {
                        string[] paises = { "NI", "ES", "US" };
                        string clasificacionOriginal = "";
                        string clasificacionFinal = "+0";

                        foreach (var pais in paises)
                        {
                            var paisData = certificaciones.FirstOrDefault(c => c["iso_3166_1"]?.ToString() == pais);
                            if (paisData != null && paisData["release_dates"] != null)
                            {
                                var firstCert = paisData["release_dates"]
                                    .FirstOrDefault(c => !string.IsNullOrEmpty(c["certification"]?.ToString()));

                                if (firstCert != null)
                                {
                                    clasificacionOriginal = firstCert["certification"]?.ToString().Trim();
                                    break;
                                }
                            }
                        }

                        // Mapeo a +0, +7, +13, +17, +18
                        switch (clasificacionOriginal)
                        {
                            case "G":
                            case "TP":
                            case "0":
                            case "A":
                                clasificacionFinal = "+0";
                                break;
                            case "PG":
                            case "7":
                            case "ATP":
                                clasificacionFinal = "+7";
                                break;
                            case "PG-13":
                            case "12":
                            case "13":
                            case "B":
                                clasificacionFinal = "+13";
                                break;
                            case "R":
                            case "16":
                            case "17":
                            case "B15":
                                clasificacionFinal = "+17";
                                break;
                            case "NC-17":
                            case "18":
                            case "C":
                                clasificacionFinal = "+18";
                                break;
                            default:
                                clasificacionFinal = "+0";
                                break;
                        }

                        txtClasificacionEdad.Text = clasificacionFinal;
                    }
                    else
                    {
                        txtClasificacionEdad.Text = "+0";
                    }
                }
                else
                {
                    string script = $"mostrarAlerta('Error', 'Película no encontrada.', 'error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
        }


        private async Task<JObject> ObtenerInformacionPelicula(string titulo)
        {
            string apiKey = "1bc6db497c4e844d1abec56c8808a145"; // Reemplaza con tu clave real
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
                    string urlClasificacion = $"https://api.themoviedb.org/3/movie/{idPelicula}/release_dates?api_key={apiKey}";

                    var detalle = await client.GetStringAsync(urlDetalles);
                    var clasificacion = await client.GetStringAsync(urlClasificacion);

                    JObject datos = JObject.Parse(detalle);
                    JObject clasificacionJson = JObject.Parse(clasificacion);
                    datos["certification"] = clasificacionJson;

                    return datos;
                }
            }

            return null;
        }

    }
}
