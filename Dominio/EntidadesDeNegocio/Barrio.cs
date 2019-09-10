using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.EntidadesDeNegocio
{
    [Table("Barrio")]
    public class Barrio : IEquatable<Barrio>
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "La {0} debe tener {1} caracteres de máximo y {2} de mínimo")]
        [Index(IsUnique = true)]
        public string Nombre { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "La {0} debe tener {1} caracteres de máximo y {2} de mínimo")]
        public string Catastro { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "La {0} debe tener {1} caracteres de máximo y {2} de mínimo")]
        public string Descripcion { get; set; }
        
        public bool Equals(Barrio other)
        {
            return other != null && this.Nombre == other.Nombre;
        }

        public override string ToString()
        {
            return "Nombre: " + this.Nombre + " Catastro: " + this.Catastro + " Descripcion: " + this.Descripcion;
        }

        //valida que ninguno de los atributos sea null o vacios 
        public bool Validar()
        {
            return !string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Catastro) && !string.IsNullOrEmpty(Descripcion);
        }
    }
}
