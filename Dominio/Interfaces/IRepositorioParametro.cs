using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepositorioParametro
    {
        Parametros FindByName(string nombre);
        IEnumerable<Parametros> FindAllParametro();
        bool AddParametro(Parametros P);
    }
}
