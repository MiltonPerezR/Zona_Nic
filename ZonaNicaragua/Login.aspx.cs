using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using ZonaNicaragua.Models;

namespace ZonaNicaragua
{
    public partial class Login : Page
    {
        private AppDbContext _context = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (Request.Cookies["UserUID"] == null)
                //{
                //    string uid = Guid.NewGuid().ToString();
                //    Response.Cookies["UserUID"].Value = uid;
                //    Response.Cookies["UserUID"].Expires = DateTime.Now.AddYears(1); // persistente

                //    // Puedes guardar este uid en tu base de datos aquí
                //}
                //else
                //{
                //    string uid = Request.Cookies["UserUID"].Value;
                //    // Puedes usar este uid para recuperar datos del usuario
                //}
                if (Session["UsuarioId"] == null && Request.Cookies["UsuarioCookie"] != null)
                {
                    var cookie = Request.Cookies["UsuarioCookie"];
                    Session["UsuarioId"] = cookie["IdUsuario"];
                    Session["UsuarioNombre"] = cookie["Usuario"];
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text.Trim();

            var user = _context.Usuarios.FirstOrDefault(x => x.Usuario == usuario && x.Clave == clave && x.Activo == true);

            if (user != null)
            {
                Session["UsuarioId"] = user.IdUsuario;
                Session["UsuarioNombre"] = user.Usuario;

                // Guardar en cookie (persistente por 7 días)
                HttpCookie userCookie = new HttpCookie("UsuarioCookie");
                userCookie.Values["IdUsuario"] = user.IdUsuario.ToString();
                userCookie.Values["Usuario"] = user.Usuario;
                userCookie.Expires = DateTime.Now.AddDays(7); // Dura 7 días
                Response.Cookies.Add(userCookie);

                Response.Redirect("Default.aspx");
            }
            else               
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}
