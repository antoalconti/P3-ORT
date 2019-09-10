using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Dominio.Interfaces;
using Dominio.EntidadesDeNegocio;

namespace Dominio.Repositorios
{
    public class RepositorioBarrio : IRepositorioBarrio
    {
        //funciones del crud de barrios, el insertar eliminar, modificar llama a los metodos que tiene la interfaz active record
        public bool AddBarrio(Barrio B)
        {
            if (B.Validar() && B != null)
            {
                using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
                {
                    try
                    {
                        db.Barrios.Add(B);
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

        //crea la lista solo de string de los nombres de los barrios que tengo para mostrarlos antes de eliminarlos
        public List<string> listaDeNombreBarrios()
        {
            var lista = FindAllBarrio();
            List<string> nombres = new List<string>();
            foreach (Barrio elem in lista)
            {
                nombres.Add(elem.Nombre);
            }
            return nombres;
        }

        public IEnumerable<Barrio> FindAllBarrio()
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                return db.Barrios.ToList();
            }
        }
        public Barrio FindBarrioById(int id)
        {
            Barrio barrio = null;
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                try
                {
                    barrio = db.Barrios.Find(id);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + "Otro error");
                }
            }
            return barrio;
        }

        public Barrio FindByNameBarrio(string name)
        {
            Barrio barrio = null;
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                try
                {
                    barrio = db.Barrios.Where(b => b.Nombre == name).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + "Otro error");
                }
            }
            return barrio;
        }
               

    }
}
