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

                    var generosAPI = datos["genres"];
                    if (generosAPI != null && generosAPI.HasValues)
                    {
                        // Lista traducida y separada
                        var listaGeneros = new List<string>();

                        foreach (var g in generosAPI)
                        {
                            var nombre = g["name"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(nombre))
                            {
                                var subGeneros = nombre.Split('&')  // separa Action & Adventure
                                                       .Select(s => s.Trim())
                                                       .Select(s => TraducirGenero(s));

                                listaGeneros.AddRange(subGeneros);
                            }
                        }

                        // Eliminar duplicados
                        listaGeneros = listaGeneros.Distinct().ToList();

                        // Mostrar todos
                        generosss.Text = string.Join(", ", listaGeneros);

                        // Asignar el primer género como principal
                        genero.Text = listaGeneros.FirstOrDefault() ?? "Sin género";

                        // Detectar tipo automáticamente
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

                    // Imagen vertical (poster)
                    string posterPath = datos["poster_path"]?.ToString();
                    if (!string.IsNullOrEmpty(posterPath))
                    {
                        string posterUrl = $"https://image.tmdb.org/t/p/w500{posterPath}";
                        tbImagenV.Text = posterUrl;
                        imgPoster.ImageUrl = posterUrl;
                    }

                    // Imagen horizontal (backdrop)
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

        private async Task<JObject> ObtenerInformacionSerie(string titulo)
        {
            string apiKey = "1bc6db497c4e844d1abec56c8808a145"; // Reemplaza con tu API Key
            string urlBusqueda = $"https://api.themoviedb.org/3/search/tv?api_key={apiKey}&language=es-ES&query={titulo}";

            using (HttpClient client = new HttpClient())
            {
                var respuesta = await client.GetStringAsync(urlBusqueda);
                JObject json = JObject.Parse(respuesta);

                var resultados = json["results"];
                if (resultados != null && resultados.HasValues)
                {
                    var idSerie = resultados[0]["id"];
                    string urlDetalles = $"https://api.themoviedb.org/3/tv/{idSerie}?api_key={apiKey}&language=es-ES";
                    var detalle = await client.GetStringAsync(urlDetalles);
                    return JObject.Parse(detalle);
                }
            }

            return null;
        }

        private string TraducirGenero(string genero)
        {
            // Traducciones manuales de los géneros
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
