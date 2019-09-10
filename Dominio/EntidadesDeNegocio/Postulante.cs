using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.EntidadesDeNegocio
{
    public class Postulante
    {
        [Key][StringLength(9, ErrorMessage = "Debe ingresar una cédula válida entre 7 y 9 dígitos sin puntos ni guiones", MinimumLength =7)]
        public string Cedula{ get; set; }
        [StringLength(50, ErrorMessage = "Debe ingresar su nombre con un largo de 3 a 50 caracteres", MinimumLength = 2)]
        public string Nombre { get; set; }
        [StringLength(50, ErrorMessage = "Debe ingresar su apellido con un largo de 3 a 50 caracteres", MinimumLength = 2)]
        public string Apellido { get; set; }
        [LegalAge(ErrorMessage = "No eres mayor de edad")]
        public DateTime FchNac { get; set; }
        [InverseProperty("PostulantesIns")]
        public virtual List<Sorteo> SorteosIns { get; set; }
        public Usuario Usuario { get; set; }
        
        public class LegalAgeAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (DateTime.Today.Year - ((DateTime)value).Year  >18)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(null);
            }
        }

        internal bool Validar()
        {
            if ((Cedula != "") && (Nombre != "") && (Apellido != "") && (Usuario!=null)&& FchNac!=null)
                return true;
            else
                return false;
        }

    }
}
