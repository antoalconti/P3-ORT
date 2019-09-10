using AdjudicacionVivienda.Models;
using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace AdjudicacionVivienda.Controllers
{
    public class ViviendaModelController : Controller
    {
        HttpClient cliente = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Uri viviendaUri = null;
        RepositorioVivienda repoV = new RepositorioVivienda();
        RepositorioBarrio repoB = new RepositorioBarrio();

        public ViviendaModelController()
        {
            cliente.BaseAddress = new Uri("http://localhost:50451");
            viviendaUri = new Uri("http://localhost:50451/api/Hogares");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: ViviendaModel
        public ActionResult Index()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            response = cliente.GetAsync(viviendaUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var viviendas = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result;
                if (viviendas.Count() != 0)
                    return View(viviendas);
                else
                {
                    ViewBag.ResultadoAccion = "No se obtuvieron viviendas";
                    return View();
                }
            }
            else
            {
                ViewBag.ResultadoAccion = "No se obtuvieron viviendas";
                return View();
            }
        }

        // GET: ViviendaModel/ModificarViv/5
        public ActionResult ModificarViv()
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            IEnumerable<Vivienda> viviendas = repoV.FindViviendasSinSortear();
            List<ViviendaModel> vivMod = new List<ViviendaModel>();
            ViviendaModel unaVM;
            foreach (Vivienda v in viviendas) {
                unaVM = crearObjetoVivModel(v);
                vivMod.Add(unaVM);
            }
            return View(vivMod);
        }

        // GET: ViviendaModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ViviendaModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViviendaModel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ViviendaModel/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Tipo"] == null || Session["Tipo"].ToString() != "Jefe")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View();
        }

        // POST: ViviendaModel/Edit/5
        [HttpPost]
        public ActionResult Edit(ViviendaModel viv)
        {
            if (viv.Estado.ToUpper() != "HABILITADA" && viv.Estado.ToUpper() != "INHABILITADA")
            {
                ViewBag.Mensaje = "Los nuevos estados solo puede ser habilitada o inhabilitada";
                return View(viv);
            }
            else {
                try
                {
                    if (repoV.UpdateVivienda(viv.ID, viv.Estado))
                        return RedirectToAction("ModificarViv");
                    else
                        return View(viv);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + "Otro error");
                    return View(viv);
                }
            }
        }

        public ActionResult CriteriosFiltro()
        {
            if (Session["Tipo"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View();
        }

        public ActionResult FDormitorio(int? cantidad)
        {
            if (Session["Tipo"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            List<ViviendaModel> viv = new List<ViviendaModel>();
            if (cantidad != null)
            {
                response = cliente.GetAsync(viviendaUri + "/getbydormitorio/" + cantidad).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                if (viv.Count() == 0)
                    ViewBag.mensaje = "No hay viviendas con esa cantidad de dormitorios.";
            }
            else
            {
                response = cliente.GetAsync(viviendaUri).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                ViewBag.mensaje = "Lista de todas las viviendas.";
            }
            return View(viv.ToList());
        }

        public ActionResult FEstado(string estado)
        {
            if (Session["Tipo"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            List<ViviendaModel> viv = new List<ViviendaModel>();
            if (!String.IsNullOrEmpty(estado))
            {
                response = cliente.GetAsync(viviendaUri + "/getbyestado/" + estado).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                if (viv.Count() == 0)
                    ViewBag.mensaje = "No hay viviendas con ese estado.";
            }
            else
            {
                response = cliente.GetAsync(viviendaUri).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                ViewBag.mensaje = "Lista de todas las viviendas.";
            }
            return View(viv.ToList());
        }

        public ActionResult FTipo(string tipo)
        {
            if (Session["Tipo"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            List<ViviendaModel> viv = new List<ViviendaModel>();
            if (!String.IsNullOrEmpty(tipo))
            {
                response = cliente.GetAsync(viviendaUri + "/getbytipo/" + tipo).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                if (viv.Count() == 0)
                    ViewBag.mensaje = "No hay viviendas de ese tipo.";
            }
            else
            {
                response = cliente.GetAsync(viviendaUri).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                ViewBag.mensaje = "Lista de todas las viviendas.";
            }
            return View(viv.ToList());
        }

        public ActionResult FBarrio(int? idbarrio)
        {
            if (Session["Tipo"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            FBarrioModel modelo = new FBarrioModel
            {
                ListaBarrios = new SelectList(repoB.FindAllBarrio(), "Id", "Nombre")
            };
            List<ViviendaModel> viv = new List<ViviendaModel>();
            if (idbarrio > 0 && idbarrio < int.MaxValue)
            {
                response = cliente.GetAsync(viviendaUri + "/getbybarrio/" + idbarrio).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                if (viv.Count() == 0) 
                    ViewBag.mensaje = "No hay viviendas en ese barrio.";
            }
            if (idbarrio == null)
            {
                response = cliente.GetAsync(viviendaUri).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                ViewBag.mensaje = "Lista de todas las viviendas.";
            }
            modelo.viviendas = viv;
            return View(modelo);
        }

        public ActionResult FPrecio(double? min, double? max)
        {
            if (Session["Tipo"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            List<ViviendaModel> viv = new List<ViviendaModel>();
            if (min > 0 && min < int.MaxValue && max > 0 && max < int.MaxValue && min < max)
            {
                response = cliente.GetAsync(viviendaUri + "/getbyprecio/"+ min+"/"+  max).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                if (viv.Count() == 0)
                    ViewBag.mensaje = "No hay viviendas en este rango de precio.";
            }
            else
            {
                response = cliente.GetAsync(viviendaUri).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                ViewBag.mensaje = "El Rango no es correcto. Por lo tanto listamos todas las viviendas";
            }
            if (min == null || max == null) {
                 response = cliente.GetAsync(viviendaUri).Result;
                if (response.IsSuccessStatusCode)
                    viv = response.Content.ReadAsAsync<IEnumerable<ViviendaModel>>().Result.ToList();
                ViewBag.mensaje = "Lista de todas las viviendas.";
            }
            return View(viv.ToList());
        }

        public ViviendaModel crearObjetoVivModel(Vivienda v)
        {
            ViviendaModel vivmod = new ViviendaModel();
            vivmod.ID = v.ID;
            vivmod.Estado = v.Estado;
            vivmod.Calle = v.Calle;
            vivmod.NumPuerta = v.NumPuerta;
            vivmod.Barrio = repoB.FindBarrioById(v.BarrioId);
            vivmod.Descripcion = v.Descripcion;
            vivmod.CantDorm = v.CantDorm;
            vivmod.Metraje = v.Metraje;
            vivmod.CantBanios = v.CantBanios;
            vivmod.Tipo = v.Tipo;
            vivmod.Anio = v.Anio;
            vivmod.PrecioFinal = v.PrecioFinal;
            return vivmod;
        }

        // GET: ViviendaModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViviendaModel/Delete/5
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
