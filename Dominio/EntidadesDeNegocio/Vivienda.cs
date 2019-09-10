using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dominio.EntidadesDeNegocio
{
    [Table ("Vivienda")]
    public abstract class Vivienda : IEquatable<Vivienda>
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La {0} debe tener {1} caracteres de máximo y {2} de mínimo")]
        public string Calle { get; set; }
        public int NumPuerta { get; set; }
        public Barrio Barrio { get; set; }
        [ForeignKey("Barrio")]
        public int BarrioId { get; set; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "La {0} debe tener {1} caracteres de máximo y {2} de mínimo")]
        public string Descripcion { get; set; }
        public int CantBanios { get; set; }
        public int CantDorm { get; set; }
        public int Metraje { get; set; }
        public int Anio { get; set; }
        public float PrecioFinal { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El {0} debe tener {1} caracteres de máximo y {2} de mínimo")]
        public string Tipo { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El {0} debe tener {1} caracteres de máximo y {2} de mínimo")]
        public string Estado { get; set; }
        public float PrecioBase { get; set; }


        public bool Equals(Vivienda other)
        {
            return other != null && this.ID == other.ID;
        }

        public override string ToString()
        {
            return string.Format(
               " Precio Base: " + this.PrecioBase +
                " ID: " + this.ID + " Calle: " + this.Calle + " Num puerta: " + this.NumPuerta + " Barrio: " + Barrio.ToString() + " Cantidad de baños: " + this.CantBanios + " Cantidad de dormitorios: " + this.CantDorm + " Metraje: " + this.Metraje + " Año: " + this.Anio);
        }

        public abstract bool Validar();

        public abstract double CalculoPrecioFinal();

        public abstract double CalculoMontoDeCuota();

    }
}
