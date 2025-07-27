using System.Web.Mvc;
using ZonaNicaragua.Models;

namespace ZonaNicaragua.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio<Usuarios> _usuarioRepositorio;

        public UsuariosController(IUsuarioRepositorio<Usuarios> usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public ActionResult Index()
        {
            var usuarios = _usuarioRepositorio.GetAll();
            return View(usuarios);
        }
    }
}
