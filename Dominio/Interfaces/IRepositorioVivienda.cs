using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepositorioVivienda
    {
        bool AddVivienda(Vivienda V);
        bool UpdateVivienda(int id, string nuevoEstado);
        Vivienda FindByIdVivienda(int Id);
        IEnumerable<Vivienda> FindAllVivienda();
        IEnumerable<Vivienda> FindByIdBarrioYHabilitada(int idBarrio);
        IEnumerable<Vivienda> FiltrarDormitorios(int cant);
        IEnumerable<Vivienda> FiltrarEstado(string estado);
        IEnumerable<Vivienda> FiltrarTipo(string tipo);
        IEnumerable<Vivienda> FiltrarRangoPrecio(double min, double max);
        IEnumerable<Vivienda> FiltrarBarrio(int id);
        IEnumerable<Vivienda> FindViviendasSinSortear();
        double precioFinal(int id);

    }
}
