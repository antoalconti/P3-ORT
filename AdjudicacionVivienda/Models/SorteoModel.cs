using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdjudicacionVivienda.Models
{
    public class SorteoModel
    {
        public int Id { get; set; }
        [RangoFecha(ErrorMessage = "La fecha debe ser mayor a la de hoy.")]
        public DateTime Fecha { get; set; }
        public Vivienda Vivienda { get; set; }
        public bool Realizado { get; set; }
        public virtual List<Postulante> PostulantesIns { get; set; }

        public Postulante Ganador { get; set; }

        [Display(Name = "Seleccione un barrio")]
        public int? IdBarrio { get; set; }
        [Display(Name = "Barrio seleccionado")]
        public int IdBarrioSeleccionado { get; set; }

        [Display(Name = "Vivienda seleccionado")]
        public int IdViviendaSeleccionado { get; set; }
        public SelectList ListaBarrios { get; set; }
        public bool CargoSorteo { get; set; }
        public SelectList ListaViendas { get; set; }
        public SorteoModel()
        {
            ListaViendas = new SelectList(new List<string>());
        }
        public class RangoFechaAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (DateTime.Today <= ((DateTime)value))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(null);
            }
        }

    }
}