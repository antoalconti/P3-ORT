using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.EntidadesDeNegocio;
using System.Diagnostics;

namespace Dominio.Repositorios
{
    public class RepositorioParametro : IRepositorioParametro
    {
        public bool AddParametro(Parametros P)
        {
            if (P.Validar() && P != null)
            {
                using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
                {
                    try
                    {
                        db.Parametros.Add(P);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + "Otro error");
                        return false;
                    }
                }
            }
            else
                return false;
        }

        public IEnumerable<Parametros> FindAllParametro()
        {
            throw new NotImplementedException();
        }

        public Parametros FindByName(string nombre)
        {
            Parametros p = null;
            if (nombre != "")
            {
                using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
                {
                    try
                    {
                        p = db.Parametros.Find(nombre);
                        return p;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + "Otro error");
                    }
                }
            }
            return p;
        }
    }
}
