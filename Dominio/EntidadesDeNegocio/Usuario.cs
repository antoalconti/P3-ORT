using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.EntidadesDeNegocio
{
    public class Usuario : IEquatable<Usuario>
    {
        public string Rol { get; set; }
        [Key]
        [DataType(DataType.EmailAddress, ErrorMessage = "e-mail con formato inválido")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(32, ErrorMessage = "La contraseña debe contener al menos 6 dígitos, una mayúscula, un dígito y un caracter alfabético", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "La contraseña debe contener al menos 6 dígitos, una mayúscula, un dígito y un caracter alfabético")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Contra { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Mail: " + this.Mail + " Contraseña: " + this.Contra);
        }

        public bool Equals(Usuario other)
        {
            return other != null && this.Mail == other.Mail;
        }

        public bool Validar()
        {
            return !string.IsNullOrEmpty(Rol) && !string.IsNullOrEmpty(Mail) && !string.IsNullOrEmpty(Contra);
        }
    }
}
