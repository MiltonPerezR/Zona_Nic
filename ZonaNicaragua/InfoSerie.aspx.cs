using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZonaNicaragua.Models;

namespace ZonaNicaragua
{
    public partial class InfoSerie : System.Web.UI.Page
    {
        public static AppDbContext Uow;

        public static string sID = "-1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    Uow = new AppDbContext();

                    sID = Request.QueryString["Id"].ToString();
                    MostrarDato();
                    
                }
            }
        }
        protected void ddlTemporadas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí va la lógica cuando se cambia la temporada.
            // Por ejemplo, cargar los episodios de la temporada seleccionada.
            int TemporadaSeleccionada = int.Parse(ddlTemporadas.SelectedValue);

            // Llama a tu método para cargar episodios o lo que necesites.
            CargarEpisodios(TemporadaSeleccionada);
        }

        private void CargarEpisodios(int NumeroTemporada)
        {
            int i = int.Parse(sID.ToString());
            var episodios = Uow.Episodios
                .Where(e => e.IdSerieE == i && e.NumeroTemporada == NumeroTemporada)
                .OrderBy(e => e.NumeroEpisodio)
                .Select(e => new
                {
                    e.IdEpisodio,
                    ImagenUrl = e.Miniatura,
                    Numero = e.NumeroEpisodio,
                    Titulo = e.TituloEpisodio,
                    e.Descripcion,
                    Duracion = e.TiempoEpisodio
                })
                
                .ToList();


            rptEpisodios.DataSource = episodios;
            rptEpisodios.DataBind();
        }
        private void MostrarDato()
        {
            int idSerie = int.Parse(sID.ToString());

            var serie = Uow.Series.FirstOrDefault(ss => ss.IdSerie == idSerie);
            int idUser = int.Parse(Session["UsuarioId"].ToString());

            var imagen = Uow.M_IMAGENHS.FirstOrDefault(i => i.IdSerieH == idSerie);
            var tipo = Uow.TipoVideos.FirstOrDefault(t => t.IdTipoVideo == serie.IdTipoVideo);

            
            imagePrincipal.ImageUrl = imagen.UrlImagenHS;
            lblTitle.Text = serie.TituloSerie;
            Sinopsis.Text = serie.SinopsisSerie;
            lblIdTipos.Text = tipo.TipoVideo;

            var s = Uow.Temporadas.Where(t => t.IdSerie == idSerie)
                .GroupBy(t => t.NombreTemporada)
                .Select(g => g.FirstOrDefault())
                .ToList();

            ddlTemporadas.DataSource = s;
            ddlTemporadas.DataTextField = "NombreTemporada";
            ddlTemporadas.DataValueField = "NumeroTemporada";
            ddlTemporadas.DataBind();

            if (ddlTemporadas.Items.Count > 0 && int.TryParse(ddlTemporadas.SelectedValue, out int te))
            {
                CargarEpisodios(te);
            }

        }
        protected void rptEpisodios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerEpisodio")
            {
                int idSerie = int.Parse(sID.ToString());

                int idEpisodio = Convert.ToInt32(e.CommandArgument);
                var epi = Uow.Episodios.FirstOrDefault(ep => ep.IdEpisodio == idEpisodio);

                var s = Uow.Series.FirstOrDefault(e1 => e1.IdSerie == idSerie);
                s.IdEpisodioQuedo = idEpisodio;
                Uow.SaveChanges();

                string userAgent = Request.UserAgent.ToLower();
                if (Request.Browser.IsMobileDevice)
                {
                    Response.Redirect($"Embe.aspx?Id={epi.IdEpisodio}&tipo=2");

                }
                if (userAgent.Contains("android"))
                {
                    Response.Redirect($"Embe.aspx?Id={epi.IdEpisodio}&tipo=2");
                }
                if (userAgent.Contains("smarttv") || userAgent.Contains("googletv") || userAgent.Contains("smart-tv"))
                {
                    Response.Redirect($"Embe.aspx?Id={epi.IdEpisodio}&tipo=2");
                }
                else
                {
                    Response.Redirect($"Embe.aspx?Id={epi.IdEpisodio}&tipo=2");
                }
            }
        }

        protected void btnVerAhora_Click(object sender, EventArgs e)
        {
            int idSerie = int.Parse(sID.ToString());
            var epi = Uow.Series.FirstOrDefault(e1 => e1.IdSerie == idSerie);
            var episodio = Uow.Episodios.FirstOrDefault(ee1 => ee1.IdEpisodio == epi.IdEpisodioQuedo);

            Response.Redirect($"Embe.aspx?Id={episodio.IdEpisodio}&tipo=2");

        }

        protected void btnLista_Click(object sender, EventArgs e)
        {

        }

        protected void btnver2_Click(object sender, EventArgs e)
        {
            int idSerie = int.Parse(sID.ToString());
            var epi = Uow.Series.FirstOrDefault(e1 => e1.IdSerie == idSerie);
            var episodio = Uow.Episodios.FirstOrDefault(ee1 => ee1.IdEpisodio == epi.IdEpisodioQuedo);

            Response.Redirect($"Reproducir.aspx?id={epi.IdEpisodioQuedo}&tipo=2");

        }
    }
}