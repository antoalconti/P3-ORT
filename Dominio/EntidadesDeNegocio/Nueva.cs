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
    [Table("Nueva")]
    public class Nueva : Vivienda
    {
        Repositorios.RepositorioParametro repop = new Repositorios.RepositorioParametro();
        Repositorios.RepositorioVivienda repoV = new Repositorios.RepositorioVivienda();

        public override string ToString()
        {
            return base.ToString();
        }

        //verifica que no se ingresen datos vacios, que el tipo de dato sea correcto y que los números sean positivos 
        public override bool Validar()
        {
            int topeMetros = Convert.ToInt32(repop.FindByName("topeMts").Valor);
            if ((PrecioBase >= 0 && PrecioBase < int.MaxValue) &&
                (PrecioFinal >= 0 && PrecioFinal < int.MaxValue) &&
                (CantBanios > 0 && CantBanios < int.MaxValue) &&
                (CantDorm > 0 && CantDorm < int.MaxValue) &&
                (Calle != "") &&
                (NumPuerta > 0 && NumPuerta < int.MaxValue) &&
                (Barrio != null) &&
                (BarrioId >= 0 && BarrioId < int.MaxValue) &&
                (Descripcion != "") &&
                (Estado != "") &&
                (Tipo != "") &&
                ((2019 - Anio) < 3) && 
                (Metraje <= topeMetros))
                return true;
            else
                return false;
        }
        
        //devuelve el calculo del precio final 
        public override double CalculoPrecioFinal()
        {
            double cotizacion = double.Parse((repop.FindByName("cotizacionUI").Valor).ToString(), CultureInfo.InvariantCulture);
            return this.PrecioBase / cotizacion;
        }

        //calcula el costo de la couta por mes basandose en el precio final de arriba
        public override double CalculoMontoDeCuota()
        {
            int i = Convert.ToInt32(repop.FindByName("interesAnual").Valor);
            int t = Convert.ToInt32(repop.FindByName("plazoNueva").Valor);
            double precioFinal = repoV.precioFinal(this.ID);
            return (precioFinal * Math.Pow((1 + i / 100.0), t)) / (t * 12);
        }
    }
}
