using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdjudicacionVivienda.Controllers
{
    public class ArchivosTxtController : Controller
    {
        RepositorioBarrio repoB = new RepositorioBarrio();
        // GET: ArchivosTxt
        public ActionResult Index()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else
                return View();
        }

        // GET: ArchivosTxt/Details/5
        public ActionResult Barrio ()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (ArchivosTxt.LeerBarrioYAgregar())
            {
                ViewBag.Mensaje = "Se Cargaron los barrios correctamente";
                return RedirectToAction("ListarBarrio");
             }
            else {
                ViewBag.Mensaje = "No se Cargaron correctamente";
                return View("Index");
            }
        }
        // GET: ArchivosTxt/Details/5
        public ActionResult Vivienda()

        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (ArchivosTxt.LeerViviendaYAgregar())
            {
                ViewBag.Mensaje = "Se Cargaron las viviendas correctamente";
                return RedirectToAction("Index", "ViviendaModel");
            }

            else {
                ViewBag.Mensaje = "No se Cargaron correctamente";
                return View("Index");
            }
                
        }

        public ActionResult Parametro()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (ArchivosTxt.LeerParametroYAgregar())
                ViewBag.Mensaje = "Se Cargaron los parametros correctamente";
            else
                ViewBag.Mensaje = "No se Cargaron correctamente";
            return View("Index");
        }
        // GET: ArchivosTxt/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: ArchivosTxt/Details/5
        public ActionResult ListarBarrio()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            IEnumerable<Barrio> listBarrios = repoB.FindAllBarrio();
            return View(listBarrios);
        }
        // POST: ArchivosTxt/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ArchivosTxt/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArchivosTxt/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ArchivosTxt/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArchivosTxt/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
