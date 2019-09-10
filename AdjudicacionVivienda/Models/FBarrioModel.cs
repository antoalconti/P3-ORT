using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdjudicacionVivienda.Models
{
    public class FBarrioModel
    {
        [Display(Name = "Seleccione un barrio")]
        public int? IdBarrio { get; set; }
        public SelectList ListaBarrios { get; set; }
        public IList<ViviendaModel> viviendas { get; set; }
    }
}