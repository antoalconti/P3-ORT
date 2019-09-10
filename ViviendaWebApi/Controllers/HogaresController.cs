using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViviendaWebApi.Models;

namespace ViviendaWebApi.Controllers
{
    [RoutePrefix("api/Hogares")]
    public class HogaresController : ApiController
    {
        RepositorioVivienda repoViv = new RepositorioVivienda();
        RepositorioBarrio repoB = new RepositorioBarrio();
        RepositorioParametro repoP = new RepositorioParametro();

        // GET: api/Hogares
        [Route("")]
        public IHttpActionResult Get()
        {
            var viv = repoViv.FindAllVivienda();
            HogarModel hogarModel = new HogarModel();
            List<HogarModel> listhogares = new List<HogarModel>();
            foreach (Vivienda v in viv)
            {
                hogarModel = crearObjetoHogarModel(v);
                listhogares.Add(hogarModel);

            }
            if (hogarModel == null)
                return NotFound();
            return Ok(listhogares);
        }

        [HttpGet]
        [Route("getbyestado/{estado}")]
        public IHttpActionResult GetByEstado(string estado)
        {
            var viv = repoViv.FiltrarEstado(estado);
            HogarModel hogarModel = new HogarModel();
            List<HogarModel> listhogares = new List<HogarModel>();
            foreach (Vivienda v in viv)
            {
                hogarModel = crearObjetoHogarModel(v);
                listhogares.Add(hogarModel);
            }
            if (hogarModel == null)
                return NotFound();
            return Ok(listhogares);
        }

        [HttpGet]
        [Route("getbydormitorio/{cantidad}")]
        public IHttpActionResult GetByDormitorio(int cantidad)
        {
            var viv = repoViv.FiltrarDormitorios(cantidad);
            HogarModel hogarModel = new HogarModel();
            List<HogarModel> listhogares = new List<HogarModel>();
            foreach (Vivienda v in viv)
            {
                hogarModel = crearObjetoHogarModel(v);
                listhogares.Add(hogarModel);
            }
            if (hogarModel == null)
                return NotFound();
            return Ok(listhogares);
        }

        [HttpGet]
        [Route("getbytipo/{tipo}")]
        public IHttpActionResult GetByTipo(string tipo)
        {
            var viv = repoViv.FiltrarTipo(tipo);
            HogarModel hogarModel = new HogarModel();
            List<HogarModel> listhogares = new List<HogarModel>();
            foreach (Vivienda v in viv)
            {
                hogarModel = crearObjetoHogarModel(v);
                listhogares.Add(hogarModel);
            }
            if (hogarModel == null)
                return NotFound();
            return Ok(listhogares);
        }

        [HttpGet]
        [Route("getbybarrio/{idbarrio}")]
        public IHttpActionResult GetByBarrio(int idbarrio)
        {
            var viv = repoViv.FiltrarBarrio(idbarrio);
            HogarModel hogarModel = new HogarModel();
            List<HogarModel> listhogares = new List<HogarModel>();
            foreach (Vivienda v in viv)
            {
                hogarModel = crearObjetoHogarModel(v);
                listhogares.Add(hogarModel);
            }
            if (hogarModel == null)
                return NotFound();
            return Ok(listhogares);
        }

        [HttpGet]
        [Route("getbyprecio/{min}/{max}")]
        public IHttpActionResult GetByPrecio(double min, double max)
        {
            var viv = repoViv.FiltrarRangoPrecio(min, max);
            HogarModel hogarModel = new HogarModel();
            List<HogarModel> listhogares = new List<HogarModel>();
            foreach (Vivienda v in viv)
            {
                hogarModel = crearObjetoHogarModel(v);
                listhogares.Add(hogarModel);
            }
            if (hogarModel == null)
                return NotFound();
            return Ok(listhogares);
        }

        // POST: api/Hogares
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Hogares/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Hogares/5
        public void Delete(int id)
        {
        }
        public HogarModel crearObjetoHogarModel(Vivienda v)
        {
            HogarModel hogarmod = new HogarModel();
            hogarmod.ID = v.ID;
            hogarmod.Estado = v.Estado;
            hogarmod.Calle = v.Calle;
            hogarmod.NumPuerta = v.NumPuerta;
            hogarmod.Barrio = repoB.FindBarrioById(v.BarrioId);
            hogarmod.Descripcion = v.Descripcion;
            hogarmod.CantDorm = v.CantDorm;
            hogarmod.Metraje = v.Metraje;
            hogarmod.CantBanios = v.CantBanios;
            hogarmod.PrecioCuota = v.CalculoMontoDeCuota();
            hogarmod.Tipo = v.Tipo;
            hogarmod.Anio = v.Anio;
            hogarmod.PrecioFinal = v.PrecioFinal;
            if (v.Tipo == "Usada")
            {
                Usada vivUs = v as Usada;
                hogarmod.Contribucion = vivUs.Contribucion;
                hogarmod.ITP = (int)repoP.FindByName("ITP").Valor;
                hogarmod.CantidadCuotas = (int)repoP.FindByName("plazoUsada").Valor * 12;
            }
            else
            {
                hogarmod.Contribucion = -1;
                hogarmod.ITP = -1;
                hogarmod.CantidadCuotas = (int)repoP.FindByName("plazoNueva").Valor * 12;
            }
            return hogarmod;
        }
    }
}
