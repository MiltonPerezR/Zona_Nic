using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZonaNicaragua
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        private AppDbContext _context = new AppDbContext();
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
            }
        }
        private void CargaElementos()
        {
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
            string texto = txtBuscar.Text.Trim();

            if (!string.IsNullOrEmpty(texto))
            {
                var resultados = Uow.ImagenV
                    .Where(p => p.Peliculas.TituloPelicula.Contains(texto))
                    .Select(p => new
                    {
                        p.Peliculas.TituloPelicula,
                        p.Peliculas.Generos,
                        p.UrlImagenV
                    }).ToList();

                rptResultados.DataSource = resultados;
                rptResultados.DataBind();
            }
            else
            {
                rptResultados.DataSource = null;
                rptResultados.DataBind();
            }
        }

        protected void hlAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Control.aspx");
        }

        protected void lkSalir_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    Uow.Dispose();
        //}
    }
}