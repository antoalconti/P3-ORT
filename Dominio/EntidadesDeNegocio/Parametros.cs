using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.EntidadesDeNegocio
{
    public class Parametros
    {
        [Key][StringLength(25, MinimumLength =2)]
        public string Nombre { get; set; }

        public Double Valor { get; set; }

        public bool Validar()
        {
            return !string.IsNullOrEmpty(Nombre) && (Valor >= 0 && Valor < int.MaxValue);
        }
    }
}
