using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepositorioBarrio
    {
        bool AddBarrio(Barrio B);
        List<string> listaDeNombreBarrios();
        Barrio FindByNameBarrio(string name);
        Barrio FindBarrioById(int id);
        IEnumerable<Barrio> FindAllBarrio();

    }
}
