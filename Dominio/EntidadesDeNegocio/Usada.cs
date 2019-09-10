using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Dominio.EntidadesDeNegocio
{
    [Table("Usada")]
    public class Usada : Vivienda
    {
        public int Contribucion { get; set; }

        Repositorios.RepositorioParametro repop = new Repositorios.RepositorioParametro();
        Repositorios.RepositorioVivienda repoV = new Repositorios.RepositorioVivienda();

        public override string ToString()
        {
            return base.ToString() + " Contribución: " + this.Contribucion;
        }

        //verifica que no se ingresen datos vacios, que el tipo de dato sea correcto y que los números sean positivos 
        public override bool Validar()
        {
            if ((PrecioBase >= 0 && PrecioBase < int.MaxValue) &&
                (PrecioFinal >= 0 && PrecioFinal < int.MaxValue) &&
                (CantBanios > 0 && CantBanios < int.MaxValue) &&
                (CantDorm > 0 && CantDorm < int.MaxValue) &&
                (Metraje > 0 && Metraje < int.MaxValue) &&
                (Anio > 0 && Anio < int.MaxValue) &&
                (Calle != "") &&
                (NumPuerta > 0 && NumPuerta < int.MaxValue) &&
                (Barrio != null) &&
                (BarrioId >= 0 && BarrioId < int.MaxValue) &&
                (Descripcion != "") &&
                (Estado != "") &&
                (Tipo != "") &&
                (Contribucion >= 0 && Contribucion < int.MaxValue))
            {
                return true;
            }
            else
                return false;
        }

        public override double CalculoPrecioFinal()
        {
            int ITP = Convert.ToInt32(repop.FindByName("ITP").Valor);
            double cotizacion = double.Parse((repop.FindByName("cotizacionUSD").Valor).ToString(), CultureInfo.InvariantCulture);
            return (this.PrecioBase * (ITP / 100.0 + 1)) / cotizacion;
        }

        public override double CalculoMontoDeCuota()
        {
            int i = Convert.ToInt32(repop.FindByName("interesAnual").Valor);
            int t = Convert.ToInt32(repop.FindByName("plazoUsada").Valor);
            double precioFinal = repoV.precioFinal(this.ID);
            return (precioFinal* Math.Pow((1 + i / 100.0), t)) / (t * 12);
        }

        //calculo del precio base cual fue el monto que se le sumo por el itp para generar precio final
        public double CalculoMontoITP()
        {
            int ITP = Convert.ToInt32(repop.FindByName("ITP").Valor);
            double cotizacion = double.Parse((repop.FindByName("cotizacionUSD").Valor).ToString(), CultureInfo.InvariantCulture);
            return (this.PrecioBase / cotizacion * ITP / 100.0);
        }
    }
}
