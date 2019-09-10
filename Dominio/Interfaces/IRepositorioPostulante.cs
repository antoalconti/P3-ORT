using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepositorioPostulante
    {
        bool AddPostulante(Postulante P);
        Postulante FindByCedulaPostulante(string cedula);
        IEnumerable<Postulante> FindAllPostulante();
        IEnumerable<Postulante> BuscarPostulantesByIdSorteo(int idS);
        Postulante FindByMailPostulante(string mail);
    }
}
