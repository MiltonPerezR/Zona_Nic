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
    public partial class ControlPrincipal : System.Web.UI.Page
    {
        public AppDbContext Uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Uow = new AppDbContext();
                CargarUsuarios();
            }
        }
        private void CargarUsuarios()
        {
            var usuarios = Uow.Usuarios
                .Where(u => u.Activo == true)
                .Include(u => u.TipoUsuarios)
                .Select(u => new
                {
                    u.IdUsuario,
                    u.Usuario,
                    Tipo = u.TipoUsuarios != null ? u.TipoUsuarios.TipoUsuario : "Sin tipo",
                    u.Activo
                })
                .ToList();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();

        }
        protected void gvUsuario_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idUsuario = Convert.ToInt32(e.CommandArgument);
            lblId.Text = idUsuario.ToString();

            Uow = new AppDbContext();
            var user = Uow.Usuarios
                .Include(p => p.TipoUsuarios)
                .FirstOrDefault(p => p.IdUsuario == idUsuario);

            switch (e.CommandName)
            {
                case "Visualizar":
                    lblTituloModal.Text = user.Usuario;
                    usertxt.Text = user.Usuario;
                    clavetxt.Text = user.Clave;
                    tipotxt.Text = user.TipoUsuarios.TipoUsuario;
                    estadotxt.Text = user.Activo.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "var myModal = new bootstrap.Modal(document.getElementById('modalVista')); myModal.show();", true);
                    break;

                case "Editar":
                    CargarDatos(); // Asumo que llena los campos del modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalEdit", "var modal = new bootstrap.Modal(document.getElementById('modalAcciones')); modal.show();", true);
                    break;

                case "Eliminar":
                    usuarioTxt.Text = user.Usuario;
                    lblId.Text = user.IdUsuario.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalDelete", "var modal = new bootstrap.Modal(document.getElementById('mEliminar')); modal.show();", true);
                    break;
            }
        }
        private void CargarDatos()
        {
            Uow = new AppDbContext();
            int lb = int.Parse(lblId.Text);

            // Obtener el usuario e incluir su tipo de usuario
            var user = Uow.Usuarios
                .Include(u => u.TipoUsuarios)
                .FirstOrDefault(u => u.IdUsuario == lb);

            if (user != null)
            {
                // Mostrar usuario y clave
                q.Text = user.Usuario;
                w.Text = user.Clave;

                // Cargar todos los tipos de usuario al DropDownList
                var tipos = Uow.TiposUsuario.ToList();
                ddlTipoUsuario.DataSource = tipos;
                ddlTipoUsuario.DataTextField = "TipoUsuario";
                ddlTipoUsuario.DataValueField = "IdTipoUsuario";
                ddlTipoUsuario.DataBind();

                // Seleccionar el tipo del usuario actual
                if (user.IdTipoUsuario != 0)
                {
                    ddlTipoUsuario.SelectedValue = user.IdTipoUsuario.ToString();
                }

                // Mostrar estado activo
                r.Text = user.Activo.ToString();
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Uow = new AppDbContext();
            int lb = int.Parse(lblId.Text);

            var user = Uow.Usuarios
                .FirstOrDefault(u => u.IdUsuario == lb);
            var t = Uow.TiposUsuario.ToList();

            user.Usuario = q.Text;
            user.Clave = w.Text;
            user.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
            user.Activo = Convert.ToBoolean(r.Text);

            Uow.SaveChanges();

            var usuarios = Uow.Usuarios
                .Where(u => u.Activo == true)
                .Include(u => u.TipoUsuarios)
                .Select(u => new
                {
                    u.IdUsuario,
                    u.Usuario,
                    Tipo = u.TipoUsuarios != null ? u.TipoUsuarios.TipoUsuario : "Sin tipo",
                    u.Activo
                })
                .ToList();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Uow = new AppDbContext();
                int id1 = int.Parse(lblId.Text);

                var user = Uow.Usuarios.FirstOrDefault(p => p.IdUsuario == id1);

                if (user != null)
                {
                    Uow.Usuarios.Remove(user);
                    Uow.SaveChanges();

                    string script = $"mostrarAlerta('Bien', 'Película Eliminada.', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    var usuarios = Uow.Usuarios
                .Where(u => u.Activo == true)
                .Include(u => u.TipoUsuarios)
                .Select(u => new
                {
                    u.IdUsuario,
                    u.Usuario,
                    Tipo = u.TipoUsuarios != null ? u.TipoUsuarios.TipoUsuario : "Sin tipo",
                    u.Activo
                })
                .ToList();

                    gvUsuarios.DataSource = usuarios;
                    gvUsuarios.DataBind();
                }
                else
                {
                    string script = $"mostrarAlerta('Error', 'No se encontró la película.', 'error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
            catch (Exception)
            {
                string script = $"mostrarAlerta('Error', 'Error en la eliminación.', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }
    }
}