using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZonaNicaragua.Models;

namespace ZonaNicaragua
{
    public partial class addEpisodios : System.Web.UI.Page
    {
        public AppDbContext Uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Uow = new AppDbContext();
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            ddlSerie.DataSource = Uow.Series.ToList();
            ddlSerie.DataTextField = "TituloSerie";
            ddlSerie.DataValueField = "IdSerie";
            ddlSerie.DataBind();
            ddlSerie.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();

            if (!int.TryParse(lblnumeroEpisodio.Text, out int episodio) ||
                !int.TryParse(tbNumeroTemporada.Text, out int temporada) ||
                ddlSerie.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "mostrarAlerta('Error', 'Verifica los campos antes de guardar.', 'error');", true);
                return;
            }

            int serie = Convert.ToInt32(ddlSerie.SelectedValue);

            var episodioExiste = Uow.Episodios.Any(p =>
                p.TituloEpisodio == txttTitulo.Text &&
                p.NumeroEpisodio == episodio &&
                p.NumeroTemporada == temporada &&
                p.IdSerieE == serie);

            if (!episodioExiste)
            {
                var tem = new Temporadas
                {
                    NumeroTemporada = temporada,
                    NombreTemporada = $"Temporada {temporada}",
                    IdSerie = serie
                };
                Uow.Temporadas.Add(tem);
                Uow.SaveChanges();

                var nuevo = new Episodios
                {
                    TituloEpisodio = txttTitulo.Text,
                    Descripcion = txtDescripcion.Text,
                    NumeroEpisodio = episodio,
                    NumeroTemporada = temporada,
                    IdTemporadaE = tem.IdTemporada,
                    Miniatura = tbMiniatura.Text,
                    UrlVideo = lblUrl.Text,
                    TiempoEpisodio = tnTiempoEpi.Text,
                    IdSerieE = serie
                };

                Uow.Episodios.Add(nuevo);
                Uow.SaveChanges();

                ScriptManager.RegisterStartupScript(this, GetType(), "success", "mostrarAlerta('Éxito', 'Episodio guardado correctamente.', 'success');", true);
                resetCampos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warning", "mostrarAlerta('Advertencia', 'El episodio ya existe.', 'warning');", true);
            }
        }

        private void resetCampos()
        {
            txttTitulo.Text = "";
            txtDescripcion.Text = "";
            tbMiniatura.Text = "";
            lblUrl.Text = "";
            tnTiempoEpi.Text = "";
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlEpisodio.aspx");
        }

        protected async void btnBuscarEpisodio_Click(object sender, EventArgs e)
        {
            if (ddlSerie.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warning", "mostrarAlerta('Advertencia', 'Selecciona una serie.', 'warning');", true);
                return;
            }

            if (!int.TryParse(tbNumeroTemporada.Text, out int temporada) ||
                !int.TryParse(lblnumeroEpisodio.Text, out int episodio))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "mostrarAlerta('Error', 'Número de episodio o temporada inválido.', 'error');", true);
                return;
            }

            string tituloSerie = ddlSerie.SelectedItem.Text;

            JObject datos = await ObtenerInformacionEpisodio(tituloSerie, temporada, episodio);

            if (datos != null)
            {
                txttTitulo.Text = datos["name"]?.ToString();
                txtDescripcion.Text = datos["overview"]?.ToString();

                string imagen = datos["still_path"]?.ToString();
                if (!string.IsNullOrEmpty(imagen))
                {
                    string url = $"https://image.tmdb.org/t/p/w500{imagen}";
                    tbMiniatura.Text = url;

                    if (!string.IsNullOrEmpty(url))
                    {
                        imgPreview.ImageUrl = url;
                    }
                }

                int? duracion = datos["runtime"]?.ToObject<int?>();
                tnTiempoEpi.Text = duracion.HasValue && duracion > 0
                    ? $"{duracion} min"
                    : "Duración no disponible";

            }
            else
            {
                string script = $"mostrarAlerta('Éxito', 'Episodio no encontrado.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }

        private async Task<JObject> ObtenerInformacionEpisodio(string tituloSerie, int temporada, int episodio)
        {
            try
            {
                string apiKey = "1bc6db497c4e844d1abec56c8808a145";
                string busquedaUrl = $"https://api.themoviedb.org/3/search/tv?api_key={apiKey}&language=es-ES&query={HttpUtility.UrlEncode(tituloSerie)}";

                using (HttpClient client = new HttpClient())
                {
                    var respuesta = await client.GetStringAsync(busquedaUrl);
                    JObject resultadoBusqueda = JObject.Parse(respuesta);

                    var idSerie = resultadoBusqueda["results"]?.FirstOrDefault()?["id"]?.ToString();

                    if (!string.IsNullOrEmpty(idSerie))
                    {
                        string urlDetalle = $"https://api.themoviedb.org/3/tv/{idSerie}/season/{temporada}/episode/{episodio}?api_key={apiKey}&language=es-ES";
                        var detalle = await client.GetStringAsync(urlDetalle);
                        return JObject.Parse(detalle);
                    }
                }
            }
            catch (Exception)
            {
                // Log o manejo opcional
            }

            return null;
        }

        protected void btnCerrar_Click1(object sender, EventArgs e)
        {
            Response.Redirect("ControlEpisodio.aspx");
        }
    }
}
