using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
namespace AdjudicacionVivienda.Controllers
{
    public class PostulanteController : Controller
    {
        HttpClient cliente = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Uri postulanteUri = null;

        public PostulanteController()
        {
            cliente.BaseAddress = new Uri("http://localhost:22584");
            postulanteUri = new Uri("http://localhost:22584/api/Participante");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        RepositorioPostulante repo = new RepositorioPostulante();

        public ActionResult Index()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Postulante")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View();
        }

        // GET: Postulante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Postulante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Postulante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Postulante unPostulante)
        {
            if (unPostulante != null)
            {
                var tareaPost = cliente.PostAsJsonAsync(postulanteUri, unPostulante);
                var result = tareaPost.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login","Usuario");
                }
                else
                {
                    ViewBag.Mensaje = "No quedaste registrado, verifica que sean datos validos.";
                    return View(unPostulante);
                }
            }
            else
            {
                TempData["ResultadoOperacion"] = "Ups! Verifique los datos";
                return View(unPostulante);
            }
        }

        // GET: Postulante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Postulante/Edit/5
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

        // GET: Postulante/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Postulante/Delete/5
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
