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
    public partial class addSerie : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                CargarDatos();
        }

        private void CargarDatos()
        {
            ddlTipoVideo.DataSource = Uow.TipoVideos.ToList();
            ddlTipoVideo.DataTextField = "TipoVideo";
            ddlTipoVideo.DataValueField = "IdTipoVideo";
            ddlTipoVideo.DataBind();
            ddlTipoVideo.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
        }

        private void campos()
        {
            if (string.IsNullOrEmpty(txttTitulo.Text))
            {
                string script = $"mostrarAlerta('warning', 'El campo de título está vacío.', 'warning');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                return;
            }
            if (string.IsNullOrEmpty(tbImagenV.Text))
            {
                string script = $"mostrarAlerta('warning', 'El campo de imagen vertical está vacío.', 'warning');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            campos();
            var serieExiste = Uow.Series.Where(p => p.TituloSerie == txttTitulo.Text).ToList();
            if (serieExiste.Count == 0)
            {
                var addSerie = new Series
                {
                    TituloSerie = txttTitulo.Text,
                    SinopsisSerie = txtSinopsis.Text,
                    FechaEstreno = estreno.Text,
                    ClasificacionEdad = txtClasificacionEdad.Text,
                    IdTipoVideo = int.Parse(ddlTipoVideo.SelectedValue),
                    Genero = genero.Text,
                    Generos = generosss.Text,
                    IdEpisodioQuedo = 1
                };

                Uow.Series.Add(addSerie);
                Uow.SaveChanges();

                var addImagenVS = new M_IMAGENVS
                {
                    UrlImagenVS = tbImagenV.Text,
                    EstadoImagenVS = true,
                    IdSerieV = addSerie.IdSerie
                };
                Uow.M_IMAGENVS.Add(addImagenVS);

                var addImagenHS = new M_IMAGENHS
                {
                    UrlImagenHS = tbImagenH.Text,
                    EstadoImagenHS = true,
                    IdSerieH = addSerie.IdSerie
                };
                Uow.M_IMAGENHS.Add(addImagenHS);

                Uow.SaveChanges();
                resetCampos();

                string script = $"mostrarAlerta('Bien', 'Serie: {addSerie.TituloSerie} guardado en la base de datos.', 'success');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
            else
            {
                string script = $"mostrarAlerta('Error', 'El  título ya existe en la base de datos.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }

        private void resetCampos()
        {
            txttTitulo.Text = "";
            txtSinopsis.Text = "";
            tbImagenV.Text = "";
            tbImagenH.Text = "";
            genero.Text = "";
            generosss.Text = "";
            estreno.Text = "";
            txtClasificacionEdad.Text = "";
            ddlTipoVideo.ClearSelection();
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlSerie.aspx");
        }

        protected async void btnBuscar_Click(object sender, EventArgs e)
        {
            string titulo = txttTitulo.Text.Trim();
            if (!string.IsNullOrEmpty(titulo))
            {
                var datos = await ObtenerInformacionSerie(titulo);

                if (datos != null)
                {
                    txttTitulo.Text = datos["name"]?.ToString();
                    txtSinopsis.Text = datos["overview"]?.ToString();

                    // Fecha de estreno
                    string fechaEstreno = datos["first_air_date"]?.ToString();
                    if (!string.IsNullOrEmpty(fechaEstreno))
                    {
                        DateTime fecha;
                        if (DateTime.TryParse(fechaEstreno, out fecha))
                        {
                            estreno.Text = fecha.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            estreno.Text = "";
                        }
                    }

                    // Extraer clasificación por edades
                    string clasificacionEdad = ObtenerClasificacionEdad(datos);
                    txtClasificacionEdad.Text = clasificacionEdad;

                    var generosAPI = datos["genres"];
                    if (generosAPI != null && generosAPI.HasValues)
                    {
                        var listaGeneros = new List<string>();

                        foreach (var g in generosAPI)
                        {
                            var nombre = g["name"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(nombre))
                            {
                                var subGeneros = nombre.Split('&')
                                                       .Select(s => s.Trim())
                                                       .Select(s => TraducirGenero(s));

                                listaGeneros.AddRange(subGeneros);
                            }
                        }

                        listaGeneros = listaGeneros.Distinct().ToList();
                        generosss.Text = string.Join(", ", listaGeneros);
                        genero.Text = listaGeneros.FirstOrDefault() ?? "Sin género";

                        string tipoDetectado = "Serie";
                        var lower = listaGeneros.Select(g => g.ToLower()).ToList();

                        if (lower.Contains("animación") && lower.Contains("acción"))
                            tipoDetectado = "Anime";
                        else if (lower.Contains("animación"))
                            tipoDetectado = "Serie Animada";
                        else if (lower.Contains("documental"))
                            tipoDetectado = "Documental";

                        var tipoItem = ddlTipoVideo.Items
                            .Cast<ListItem>()
                            .FirstOrDefault(i => i.Text.Equals(tipoDetectado, StringComparison.OrdinalIgnoreCase));

                        if (tipoItem != null)
                        {
                            ddlTipoVideo.ClearSelection();
                            tipoItem.Selected = true;
                        }
                    }

                    // Imagen vertical
                    string posterPath = datos["poster_path"]?.ToString();
                    if (!string.IsNullOrEmpty(posterPath))
                    {
                        string posterUrl = $"https://image.tmdb.org/t/p/w500{posterPath}";
                        tbImagenV.Text = posterUrl;
                        imgPoster.ImageUrl = posterUrl;
                    }

                    // Imagen horizontal
                    string backdropPath = datos["backdrop_path"]?.ToString();
                    if (!string.IsNullOrEmpty(backdropPath))
                    {
                        string backdropUrl = $"https://image.tmdb.org/t/p/w780{backdropPath}";
                        tbImagenH.Text = backdropUrl;
                        imgPosterH.ImageUrl = backdropUrl;
                    }
                }
            }
        }

        private string ObtenerClasificacionEdad(JObject datos)
        {
            // Intentar obtener las clasificaciones de contenido
            var ratings = datos["content_ratings"]?["results"];
            if (ratings != null && ratings.HasValues)
            {
                // Buscar clasificación para España "ES"
                var ratingES = ratings.FirstOrDefault(r => r["iso_3166_1"]?.ToString() == "ES");
                if (ratingES != null)
                    return TraducirClasificacion(ratingES["rating"]?.ToString());

                // Si no existe España, obtener la primera clasificación válida
                var primera = ratings.FirstOrDefault(r => !string.IsNullOrEmpty(r["rating"]?.ToString()));
                if (primera != null)
                    return TraducirClasificacion(primera["rating"]?.ToString());
            }

            return "Todos"; // valor por defecto si no encuentra nada
        }

        private string TraducirClasificacion(string rating)
        {
            // Mapear las clasificaciones TMDb a las que quieres mostrar
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"TV-Y", "+0"},
                {"TV-Y7", "+7"},
                {"TV-G", "+0"},
                {"TV-PG", "+7"},
                {"TV-14", "+13"},
                {"TV-MA", "+17"},
                {"G", "+0"},
                {"PG", "+7"},
                {"PG-13", "+13"},
                {"R", "+17"},
                {"NC-17", "+18"},
                {"NR", "Todos"},
                {"Unrated", "Todos"}
            };

            if (string.IsNullOrEmpty(rating))
                return "Todos";

            return map.ContainsKey(rating) ? map[rating] : rating;
        }

        private async Task<JObject> ObtenerInformacionSerie(string titulo)
        {
            string apiKey = "1bc6db497c4e844d1abec56c8808a145";
            string urlBusqueda = $"https://api.themoviedb.org/3/search/tv?api_key={apiKey}&language=es-ES&query={titulo}";

            using (HttpClient client = new HttpClient())
            {
                var respuesta = await client.GetStringAsync(urlBusqueda);
                JObject json = JObject.Parse(respuesta);

                var resultados = json["results"];
                if (resultados != null && resultados.HasValues)
                {
                    var idSerie = resultados[0]["id"];
                    string urlDetalles = $"https://api.themoviedb.org/3/tv/{idSerie}?api_key={apiKey}&language=es-ES&append_to_response=content_ratings";
                    var detalle = await client.GetStringAsync(urlDetalles);
                    return JObject.Parse(detalle);
                }
            }

            return null;
        }

        private string TraducirGenero(string genero)
        {
            var traducciones = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"Action", "Acción"},
                {"Adventure", "Aventura"},
                {"Animation", "Animación"},
                {"Comedy", "Comedia"},
                {"Crime", "Crimen"},
                {"Documentary", "Documental"},
                {"Drama", "Drama"},
                {"Family", "Familiar"},
                {"Fantasy", "Fantasía"},
                {"History", "Historia"},
                {"Horror", "Terror"},
                {"Music", "Música"},
                {"Mystery", "Misterio"},
                {"Romance", "Romance"},
                {"Science Fiction", "Ciencia Ficción"},
                {"TV Movie", "Película de TV"},
                {"Thriller", "Suspenso"},
                {"War", "Guerra"},
                {"Western", "Occidental"},
                {"Kids", "Infantil"},
                {"Reality", "Reality Show"},
                {"News", "Noticias"},
                {"Soap", "Novela"}
            };

            return traducciones.ContainsKey(genero) ? traducciones[genero] : genero;
        }
    }
}
