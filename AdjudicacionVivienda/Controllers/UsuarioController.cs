using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Dominio.Context;
using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;

namespace AdjudicacionVivienda.Controllers
{
    public class UsuarioController : Controller
    {
        RepositorioUsuario repoU = new RepositorioUsuario();
        // GET: Usuario
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuario/Login
        [HttpPost]
        public ActionResult Login(string DatoLogin, string PasswordLogin)
        {
            Usuario usr = repoU.FindUsuario(DatoLogin, PasswordLogin);
            if (usr != null)
            {
                Session["Tipo"] = usr.Rol;
                Session["Email"] = usr.Mail;
                if (usr.Rol == "Postulante")
                    return RedirectToAction("Index", "Home");
                if (usr.Rol == "Jefe")
                    return RedirectToAction("Index", "Home");
            }
            ViewBag.Mensaje = "El mail o contraseña son inexistentes";
            return View();
        }
        public ActionResult Logout()
        {
            Session["Tipo"] = null;
            Session["Email"] = null;
            return RedirectToAction("Login", "Usuario");
        }

    }
}