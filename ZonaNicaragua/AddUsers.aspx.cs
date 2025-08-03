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
        private bool campos()
        {
            if (txtUsuario.Text.Length == 0)
            {
                string script = $"mostrarAlerta('Error', 'El campo de usuario no debe de estar vacío.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                return true;
            }
            if (txtClave.Text.Length == 0)
            {
                string script = $"mostrarAlerta('Error', 'El campo de clave no debe de estar vacío.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                return true;
            }
            if (ddlTipoUsuario.SelectedValue.ToString() == "0")
            {
                string script = $"mostrarAlerta('Error', 'Debes de seleccionar un tipo de usuario.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                return true;
            }
            return false;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool vCampo = campos();
            if (!vCampo)
            {
                Uow = new AppDbContext();
                string usuarioText = txtUsuario.Text.ToUpper();
                var usuario = Uow.Usuarios.Where(u => u.Usuario == usuarioText).ToList();

                if (usuario != null)
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

                    string script = $"mostrarAlerta('Éxito', 'Usuario creado.', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
        }
        private void resetCampos()
        {
            txtUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;
            ddlTipoUsuario.ClearSelection();
            CargarDatos();
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlPrincipal.aspx");
        }
    }
}