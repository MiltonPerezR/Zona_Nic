using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZonaNicaragua.Models;
using System.Data.Entity;

namespace ZonaNicaragua
{
    public partial class Default : Page
    {
        public AppDbContext Uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Uow = new AppDbContext();
                CargarBannerPrincipal();
                CargarPeliculasPorGeneros();
                peliculasRelacionadas();

            }
        }
        public void peliculasRelacionadas()
        {
            DateTime fechaLimite = DateTime.Now.AddMonths(-1).AddDays(-15);
            DateTime fechaActual = DateTime.Now;

            // Consulta de películas
            var peliculas = Uow.Peliculas
                .Include(p => p.ImagenV)
                .ToList() // Aquí lo traes a memoria para poder usar C# puro
                .Select(p => new
                {
                    Id = p.IdPelicula,
                    Titulo = p.TituloPelicula,
                    Imagen = p.ImagenV.Select(iv => iv.UrlImagenV).FirstOrDefault() ?? "/imagenes/no-disponible.jpg",
                    FechaEstreno = DateTime.TryParse(p.FechaEstreno, out var fecha) ? fecha.Year.ToString() : "",
                    Calidad = p.Calidad,
                    Genero = p.Genero,
                    Tipo = "Pelicula"
                })
                .ToList()
                .Where(pl => DateTime.TryParse(pl.FechaEstreno, out DateTime fechaEstreno) &&
                             fechaEstreno >= fechaLimite &&
                             fechaEstreno <= fechaActual);

            // Consulta de series
            var series = Uow.Series
                .Include(s => s.M_IMAGENVS)
                .Select(s => new
                {
                    Id = s.IdSerie,
                    Titulo = s.TituloSerie,
                    Imagen = s.M_IMAGENVS.Select(iv => iv.UrlImagenVS).FirstOrDefault() ?? "/imagenes/no-disponible.jpg",
                    FechaEstreno = s.FechaEstreno,
                    Calidad = "HD",
                    Genero = s.Genero,
                    Tipo = "Serie"
                })
                .ToList()
                .Where(sr => DateTime.TryParse(sr.FechaEstreno, out DateTime fechaEstreno) &&
                             fechaEstreno >= fechaLimite &&
                             fechaEstreno <= fechaActual);

            // Unir resultados
            var contenidoRelacionado = peliculas
                .Concat(series)
                .OrderByDescending(x => x.Id)
                .Take(10)
                .OrderBy(x => Guid.NewGuid())
                .ToList();

            rptSugerencias.DataSource = contenidoRelacionado;
            rptSugerencias.DataBind();
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
        private void CargarPeliculasPorGeneros()
        {
            var peliculas = Uow.ImagenV
                .Include("Peliculas")
                .Where(p => p.Peliculas != null)
                .ToList();

            string url;

            // Obtener el total de películas
            int totalPeliculas = Uow.Peliculas.Count();

            // Generar un índice aleatorio entre 0 y totalPeliculas-1
            Random rnd = new Random();
            int indexAleatorio = rnd.Next(totalPeliculas);

            // Obtener la película aleatoria
            var peliculaAleatoria = Uow.Peliculas
                                     .OrderBy(p => p.IdPelicula)
                                     .Skip(indexAleatorio)
                                     .FirstOrDefault();

            if (peliculaAleatoria != null)
            {
                lblId.Text = peliculaAleatoria.IdPelicula.ToString();
                lblTitleP.Text = peliculaAleatoria.TituloPelicula;

                string userAgent = Request.UserAgent.ToLower();
                if (userAgent.Contains("android") || userAgent.Contains("smarttv") || userAgent.Contains("googletv") || userAgent.Contains("smart-tv"))
                {
                    //var imaV = Uow.ImagenV.FirstOrDefault(p => p.IdPeliculaV == peliculaAleatoria.IdPelicula);
                    //url = imaV != null ? imaV.UrlImagenV : "";

                    var ima = Uow.M_IMAGENH.FirstOrDefault(p => p.IdPeliculaH == peliculaAleatoria.IdPelicula);
                    url = ima != null ? ima.UrlImagenH : "";
                }
                else
                {
                    var ima = Uow.M_IMAGENH.FirstOrDefault(p => p.IdPeliculaH == peliculaAleatoria.IdPelicula);
                    url = ima != null ? ima.UrlImagenH : "";
                }
                bannerPrincipal.Style["background-image"] = $"url('{url}')";

                // Obtener todas las series con su género
                var series = Uow.Series
                    .ToList();

                // Lista combinada películas + series
                var items = new List<dynamic>();

                // Agregar películas
                // Agregar películas con campo Genero (string)
                foreach (var p in peliculas)
                {
                    items.Add(new
                    {
                        Genero = string.IsNullOrEmpty(p.Peliculas?.Genero) ? "Sin Género" : p.Peliculas.Genero,
                        Peliculas = new
                        {
                            IdPelicula = p.Peliculas.IdPelicula,
                            TituloPelicula = p.Peliculas.TituloPelicula,
                            Genero = p.Peliculas.Genero,
                            Calidad = p.Peliculas.Calidad
                        },
                        UrlImagenV = p.UrlImagenV,
                        Tipo = "Pelicula"
                    });
                }

                // Agregar series simulando estructura de película
                foreach (var s in series)
                {
                    var ima1 = Uow.M_IMAGENVS.FirstOrDefault(p => p.IdSerieV == s.IdSerie);
                    var imagenUrl = ima1 != null ? ima1.UrlImagenVS : "";

                    items.Add(new
                    {
                        Genero = string.IsNullOrEmpty(s.Genero)
                                    ? "Sin Género"
                                    : s.Genero,
                        Peliculas = new
                        {
                            IdPelicula = s.IdSerie,
                            TituloPelicula = s.TituloSerie,
                            Generos = s.Generos,
                            Calidad = "HD"
                        },
                        UrlImagenV = imagenUrl,
                        Tipo = "Serie"
                    });
                }

                // Agrupar por género y limitar a 10 elementos aleatorios por género
                var generosConPeliculas = items
                    .GroupBy(i => i.Genero)
                    .Select(g => new
                    {
                        Genero = g.Key,
                        Peliculas = g.OrderBy(x => Guid.NewGuid()) // Orden aleatorio
                                     .Take(25)
                                     .ToList()
                    })
                    .ToList();

                rptGeneros.DataSource = generosConPeliculas;
                rptGeneros.DataBind();
            }
        }

        protected void timerBanner_Tick(object sender, EventArgs e)
        {
            CargarBannerPrincipal();
        }
        private void CargarBannerPrincipal()
        {
            Uow = new AppDbContext();

            int totalPeliculas = Uow.Peliculas.Count();
            if (totalPeliculas == 0) return;

            Random rnd = new Random();
            int indexAleatorio = rnd.Next(totalPeliculas);

            var peliculaAleatoria = Uow.Peliculas
                .OrderBy(p => p.IdPelicula)
                .Skip(indexAleatorio)
                .FirstOrDefault();

            if (peliculaAleatoria != null)
            {
                lblId.Text = peliculaAleatoria.IdPelicula.ToString();
                lblTitleP.Text = peliculaAleatoria.TituloPelicula;

                string url = "";
                string userAgent = Request.UserAgent.ToLower();

                if (userAgent.Contains("android") || userAgent.Contains("smarttv") || userAgent.Contains("googletv") || userAgent.Contains("smart-tv"))
                {
                    var ima = Uow.M_IMAGENH.FirstOrDefault(p => p.IdPeliculaH == peliculaAleatoria.IdPelicula);
                    url = ima != null ? ima.UrlImagenH : "";
                }
                else
                {
                    var ima = Uow.M_IMAGENH.FirstOrDefault(p => p.IdPeliculaH == peliculaAleatoria.IdPelicula);
                    url = ima != null ? ima.UrlImagenH : "";
                }

                bannerPrincipal.Style["background-image"] = $"url('{url}')";
            }
        }


        protected void rptGeneros_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptPeliculas = (Repeater)e.Item.FindControl("rptPeliculas");
                var dataItem = e.Item.DataItem;
                var peliculas = DataBinder.Eval(dataItem, "Peliculas") as List<ImagenV>;
                rptPeliculas.DataSource = peliculas;
                rptPeliculas.DataBind();
            }
        }

        protected void rptPeliculas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerPelicula")
            {
                string[] argumentos = e.CommandArgument.ToString().Split('|');
                int id = int.Parse(argumentos[0]);
                string tipo = argumentos[1]; // "Pelicula" o "Serie"

                if (tipo == "Pelicula")
                {
                    Response.Redirect("InfoPelicula.aspx?id=" + id);
                }
                else if (tipo == "Serie")
                {
                    Response.Redirect("InfoSerie.aspx?id=" + id);
                }
            }
        }

        protected void btnRepro_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();
            int idPelicula = int.Parse(lblId.Text);
            string userAgent = Request.UserAgent.ToLower();
            if (userAgent.Contains("android") || userAgent.Contains("smarttv") || userAgent.Contains("googletv") || userAgent.Contains("smart-tv"))
            {
                var pelicula = Uow.Peliculas.FirstOrDefault(p => p.IdPelicula == idPelicula);
                Response.Redirect(pelicula.UrlPelicula);
            }
            else
            {
                Response.Redirect($"Reproducir.aspx?Id={idPelicula}&tipo=1");
            }
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int idPelicula = int.Parse(lblId.Text);
            Response.Redirect($"InfoPelicula.aspx?Id={idPelicula}&tipo=1");
        }
    }
}
