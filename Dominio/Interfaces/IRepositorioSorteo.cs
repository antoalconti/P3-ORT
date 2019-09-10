using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio.Interfaces
{
    public interface IRepositorioSorteo
    {
        bool AddSorteo(Sorteo S);
        Sorteo FindByIdVivienda(int idv);
        bool InscripcionASorteoPostulante(string idPostulante, int idSorteo);
        IEnumerable<Sorteo> FindAllSorteoRealizados();
        IEnumerable<Sorteo> FindAllSorteoNoRealizados();
        bool Modificar(int idS, Postulante ganador);
       

    }
}
