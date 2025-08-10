using System;
using System.Linq;
using System.Web;
using ZonaNicaragua.Models;
using System.Globalization;
using System.Text;

namespace ZonaNicaragua
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public AppDbContext Uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Uow = new AppDbContext();
                
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                CargaElementos();

                PrecargarPeliculasYSeries();
            }
        }

        private void PrecargarPeliculasYSeries()
        {
            Uow = new AppDbContext();

            // Traer las películas con estructura, ordenando aleatoriamente
            var peliculas = Uow.ImagenV
                .Select(p => new
                {
                    Id = p.Peliculas.IdPelicula,
                    Titulo = p.Peliculas.TituloPelicula,
                    Genero = p.Peliculas.Generos,
                    UrlImagen = p.UrlImagenV,
                    Tipo = "Pelicula"
                });

            // Traer las series con estructura, ordenando aleatoriamente
            var series = Uow.M_IMAGENVS
                .Select(s => new
                {
                    Id = s.Series.IdSerie,
                    Titulo = s.Series.TituloSerie,
                    Genero = s.Series.Genero,
                    UrlImagen = s.UrlImagenVS,
                    Tipo = "Serie"
                });

            // Concatenar y ordenar aleatoriamente
            var precargados = peliculas
                .Concat(series)
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList();

            rptResultados.DataSource = precargados;
            rptResultados.DataBind();

            lblMensaje.Visible = precargados.Count == 0;
            if (precargados.Count == 0)
                lblMensaje.Text = "No hay películas ni series para mostrar.";
        }



        private void CargaElementos()
        {
            Uow = new AppDbContext();
            lblUser.Text = Session["UsuarioNombre"].ToString();
            int idUser = int.Parse(Session["UsuarioId"].ToString());
            hlAdmin.Visible = false;

            var usuarios = Uow.Usuarios.FirstOrDefault(u => u.IdTipoUsuario == idUser);
            if (usuarios != null)
            {
                hlAdmin.Visible = true;
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e); // Reutiliza la lógica del botón
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();
            string texto = txtBuscar.Text.Trim().ToLower();
            string textoNormalizado = RemoveDiacritics(texto);

            if (!string.IsNullOrEmpty(textoNormalizado))
            {
                var peliculas = Uow.ImagenV
                    .Select(p => new
                    {
                        Id = p.Peliculas.IdPelicula,
                        Titulo = p.Peliculas.TituloPelicula,
                        Genero = p.Peliculas.Generos,
                        UrlImagen = p.UrlImagenV,
                        Tipo = "Pelicula"
                    }).ToList();

                var series = Uow.M_IMAGENVS
                    .Select(s => new
                    {
                        Id = s.Series.IdSerie,
                        Titulo = s.Series.TituloSerie,
                        Genero = s.Series.Generos,
                        UrlImagen = s.UrlImagenVS,
                        Tipo = "Serie"
                    }).ToList();

                var resultados = peliculas
                    .Concat(series)
                    .Where(item =>
                    {
                        string titulo = RemoveDiacritics(item.Titulo.ToLower());
                        string[] palabrasTitulo = titulo.Split(' ');
                        string[] palabrasBuscadas = textoNormalizado.Split(' ');

                        foreach (var palabraBuscada in palabrasBuscadas)
                        {
                            foreach (var palabraTitulo in palabrasTitulo)
                            {
                                if (LevenshteinDistance(palabraBuscada, palabraTitulo) <= 1)
                                    return true;
                            }
                        }

                        return false;
                    }).ToList();

                if (resultados.Any())
                {
                    rptResultados.DataSource = resultados;
                    rptResultados.DataBind();
                    lblMensaje.Visible = false;
                }
                else
                {
                    rptResultados.DataSource = null;
                    rptResultados.DataBind();
                    lblMensaje.Text = "No se encontraron resultados.";
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                rptResultados.DataSource = null;
                rptResultados.DataBind();
                lblMensaje.Visible = false;
            }
        }

        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public int LevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s)) return t.Length;
            if (string.IsNullOrEmpty(t)) return s.Length;

            int[,] d = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++) d[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(
                        d[i - 1, j] + 1,
                        d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[s.Length, t.Length];
        }

        protected void hlAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Control.aspx");
        }

        protected void lkSalir_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["UsuarioCookie"] != null)
            {
                HttpCookie cookie = new HttpCookie("UsuarioCookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            Response.Redirect("Login.aspx");
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
