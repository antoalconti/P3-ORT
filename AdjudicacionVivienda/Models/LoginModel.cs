using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdjudicacionVivienda.Models
{
   public class LoginModel
    {
        [Display(Name = "Usuario")]
        public string DatoLogin { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]

        public string PasswordLogin { get; set; }
    }
}
