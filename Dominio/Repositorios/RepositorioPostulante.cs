using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.EntidadesDeNegocio;

namespace Dominio.Repositorios
{
    public class RepositorioPostulante : IRepositorioPostulante
    {
        public bool AddPostulante(Postulante P)
        {
            if (P.Validar() && P != null)
            {
                using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
                {
                    P.Usuario.Rol = "Postulante";
                    db.Postulantes.Add(P);
                    db.SaveChanges();
                    return true;
                }
            }
            else
                return false;
        }

        public IEnumerable<Postulante> FindAllPostulante()
        {
            throw new NotImplementedException();
        }

        public Postulante FindByCedulaPostulante(string cedula)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Postulantes.Find(cedula);
                return result;
            }
        }

        public IEnumerable<Postulante> BuscarPostulantesByIdSorteo(int idS)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Sorteos.Where(s => s.Id == idS).SelectMany(s => s.PostulantesIns).ToList().OrderBy(p => p.Nombre);
                return result;
            }
        }
        public Postulante FindByMailPostulante(string mail)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Postulantes.Where(s => s.Usuario.Mail == mail).FirstOrDefault();
                    return result;
            }
        }


        
    }
}
