using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdjudicacionVivienda.Models
{
    public class ViviendaModel
    {
        RepositorioBarrio repoB = new RepositorioBarrio();

        public string Estado { get; set; }
        public int ID { get; set; }
        public string Calle { get; set; }
        [Display(Name = "Num puerta")]
        public int NumPuerta { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Baños")]
        public int CantBanios { get; set; }
        [Display(Name = "Dormitorios")]
        public int CantDorm { get; set; }
        public int Metraje { get; set; }
        [Display(Name = "Año")]
        public int Anio { get; set; }
        public float PrecioFinal { get; set; }
        public double PrecioCuota { get; set; }
        [Display(Name = "CantCuotas")]
        public double CantidadCuotas { get; set; }
        public float ITP { get; set; }
        public float Contribucion { get; set; }
        public string Tipo { get; set; }
        public int BarrioId { get; set; }
        public Barrio Barrio { get; set; }
        [Display(Name = "Barrio")]
        public string NomBarrio { get; set; }    

    }
}