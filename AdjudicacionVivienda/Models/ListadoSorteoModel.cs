using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdjudicacionVivienda.Models
{
    public class ListadoSorteoModel
    {
        public IEnumerable<Sorteo> listaRealizados { get; set; }
        public IEnumerable<Sorteo> listaNoRealizados { get; set; }

        
    }
}