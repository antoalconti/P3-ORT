using AdjudicacionVivienda.Models;
using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdjudicacionVivienda.Controllers
{
    public class SorteoModelController : Controller
    {
        RepositorioBarrio repb = new RepositorioBarrio();
        RepositorioVivienda repV = new RepositorioVivienda();
        RepositorioSorteo repS = new RepositorioSorteo();
        RepositorioPostulante repP = new RepositorioPostulante();

        // GET: SorteoModel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrepararSorteo()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            SorteoModel modelo = new SorteoModel
            {
                ListaBarrios = new SelectList(repb.FindAllBarrio(), "Id", "Nombre")
            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult PrepararSorteo(SorteoModel sm)
        {

            RepositorioVivienda repv = new RepositorioVivienda();
            if (sm.IdBarrio != null)
            {
                var viviendas = repv.FindByIdBarrioYHabilitada((int)sm.IdBarrio);
                sm.ListaViendas = new SelectList(viviendas, "Id", "calle");
                if (viviendas.Count() == 0)
                    ViewBag.Mensaje = "No hay viviendas para sortear en ese barrio";
            }
            var barrios = repb.FindAllBarrio();
            sm.ListaBarrios = new SelectList(barrios, "Id", "Nombre");
            if (sm.IdViviendaSeleccionado != 0 && sm.CargoSorteo == true)
            {
                Vivienda viv = repv.FindByIdVivienda(sm.IdViviendaSeleccionado);
                Sorteo unS = new Sorteo
                {
                    Fecha = sm.Fecha,
                    Vivienda = viv,
                    Realizado = false
                };
                if (repS.AddSorteo(unS))
                {
                    ViewBag.MensajeGuardado = "Se guardo el sorteo correctamente.";
                    RedirectToAction("PrepararSorteo");
                }
                else
                {
                    ViewBag.MensajeGuardado = "No se guardo el sorteo, revise los datos.";
                    RedirectToAction("PrepararSorteo");
                }
            }
            return View("PrepararSorteo", sm);
        }

        // GET: SorteoModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SorteoModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SorteoModel/Create
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

        // GET: SorteoModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SorteoModel/Edit/5
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

        // GET: SorteoModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SorteoModel/Delete/5
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

        public ActionResult InscripcionSorteo(int idViv)
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Postulante")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            var sorteo = repS.FindByIdVivienda(idViv);
            var postulante = repP.FindByMailPostulante(Session["Email"].ToString());
            if (sorteo != null)
            {
                if (repS.InscripcionASorteoPostulante(postulante.Cedula, sorteo.Id))
                    ViewBag.Mensaje = "Estas inscripto al sorteo.";
                else
                    ViewBag.Mensaje = "No quedaste inscripto al sorteo.";
            }
            else
                ViewBag.Mensaje = "No existe sorteo para esa vivienda.";
            return View();
        }

        public ActionResult VerSorteo()
        {
            if (Session["Tipo"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            ListadoSorteoModel r = new ListadoSorteoModel
            {
                listaNoRealizados = repS.FindAllSorteoNoRealizados(),
                listaRealizados = repS.FindAllSorteoRealizados()
            };
            return View(r);
        }
        public ActionResult RealizarSorteo(int idS)
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            IEnumerable<Postulante> postulantes = repP.BuscarPostulantesByIdSorteo(idS);
            if (postulantes != null && postulantes.Count()>0)
            {
                var random = new Random();
                var resultado = random.Next(0, postulantes.Count() - 1);
                Postulante ganador = postulantes.ElementAt(resultado);
                if (ganador != null)
                {
                    if (repS.Modificar(idS, ganador))
                    {
                        ViewBag.Mensaje = "Se realizó el sorteo con exito! El ganador es el postulante: " + ganador.Nombre;
                        return View();
                    }
                    ViewBag.Mensaje = "No se realizó el sorteo";
                    return View();
                }
                ViewBag.Mensaje = "No se realizó el sorteo";
                return View();
            }
            ViewBag.Mensaje = "No se realizó el sorteo, no hay postulantes inscriptos al sorteo";
            return View();
        }
        

    }
}
