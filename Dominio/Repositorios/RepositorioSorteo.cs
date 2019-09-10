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
    public class RepositorioSorteo : IRepositorioSorteo
    {
        RepositorioPostulante repoP = new RepositorioPostulante();
        public bool AddSorteo(Sorteo S)
        {
            if (S.Validar() && S != null)
            {
                using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
                {
                    try
                    {
                        db.Viviendas.Attach(S.Vivienda);
                        db.Sorteos.Add(S);
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

        public Sorteo FindByIdVivienda(int idv)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Sorteos.Where(c => c.Vivienda.ID == idv).FirstOrDefault();
                return result;
            }
        }

        public bool InscripcionASorteoPostulante(string idPostulante, int idSorteo)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                try
                {
                    if (db.Sorteos.Where(c => c.Ganador.Cedula == idPostulante).FirstOrDefault() != null)
                        return false;
                    else
                    {
                        Sorteo unS = db.Sorteos.Find(idSorteo);
                        Postulante unP = db.Postulantes.Find(idPostulante);
                        unS.PostulantesIns.Add(unP);
                        unP.SorteosIns.Add(unS);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + "Otro error");
                    return false;
                }
            }
        }

        public IEnumerable<Sorteo> FindAllSorteoRealizados()
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Sorteos.Where(c => c.Realizado == true).ToList().OrderByDescending(c => c.Fecha);
                foreach (Sorteo s in result)
                {
                    s.Ganador = repoP.FindByCedulaPostulante(s.GanadorId);
                }
                return result;
            }
        }

        public IEnumerable<Sorteo> FindAllSorteoNoRealizados()
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Sorteos.Where(c => c.Realizado == false).ToList().OrderBy(c => c.Fecha);
                
                return result;
            }

        }

        public bool Modificar(int idS, Postulante ganador)
        {
            if (ganador == null)
                return false;

            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                try
                {
                    db.Postulantes.Attach(ganador);
                    var r = db.Sorteos.Find(idS);
                    r.Realizado = true;
                    r.Ganador = ganador;
                    var s = db.Sorteos.ToList();
                    foreach (Sorteo unS in s)
                    {
                        unS.PostulantesIns.Remove(ganador);
                    }
                    var v = db.Viviendas.Find(r.ViviendaId);
                    v.Estado = "Sorteada";
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + "Otro error");
                    return false;
                }
            }
            return false;
        }


    }
}
