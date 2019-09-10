using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.EntidadesDeNegocio
{
    public class Sorteo
    {
        public int Id { get; set; }
        [RangoFecha(ErrorMessage = "La fecha debe ser mayor a la de hoy.")]
        public DateTime Fecha { get; set; }
        public Vivienda Vivienda { get; set; }
        [ForeignKey("Vivienda")]
        [Index(IsUnique = true)]
        public int ViviendaId { get; set; }
        public bool Realizado { get; set; }
        public virtual List<Postulante> PostulantesIns { get; set; }
        public Postulante Ganador { get; set; }
        [ForeignKey("Ganador")]
        public string GanadorId { get; set; }


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

        public bool Validar()
        {
            return Vivienda != null && Fecha != null && (ViviendaId >= 0 && ViviendaId < int.MaxValue) && (Realizado == true || Realizado == false);
        }


    }
}
