using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZonaNicaragua.Models;

namespace ZonaNicaragua
{
    public partial class AddUsers : System.Web.UI.Page
    {
        public AppDbContext Uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Uow = new AppDbContext();
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            ddlTipoUsuario.DataSource = Uow.TiposUsuario.ToList();
            ddlTipoUsuario.DataTextField = "TipoUsuario";
            ddlTipoUsuario.DataValueField = "IdTipoUsuario";
            ddlTipoUsuario.DataBind();
            ddlTipoUsuario.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));

        }
        private void campos()
        {
            if (txtUsuario.Text.Length < 2)
            {
                lblConfirmacion.Text = "El usuario debe de contener almenos 2 caracteres";
                return;
            }
            if (txtClave.Text.Length < 2)
            {
                lblConfirmacion.Text = "La clave debe de contener almenos 2 caracteres";
                return;
            }
            if (ddlTipoUsuario.SelectedValue.ToString() == "1")
            {
                lblConfirmacion.Text = "Debes de seleccionar un tipo de usuario";
                return;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            campos();
            Uow = new AppDbContext();
            string usuarioText = txtUsuario.Text.ToUpper();
            var usuario = Uow.Usuarios.Where(u => u.Usuario == usuarioText ).ToList();

            if(usuario != null)
            {
                var userAdd = new Usuarios
                {
                    Usuario = usuarioText,
                    Clave = txtClave.Text,
                    Activo = true,
                    IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue)
                };

                Uow.Usuarios.Add(userAdd);

                Uow.SaveChanges();
                resetCampos();
            }
        }
        private void resetCampos()
        {
            txtUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;
            ddlTipoUsuario.DataSource = null;
            ddlTipoUsuario.DataBind(); ;
        }
    }
}