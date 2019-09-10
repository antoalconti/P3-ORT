using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.EntidadesDeNegocio;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Dominio.Repositorios
{
    public class RepositorioVivienda : IRepositorioVivienda
    {
        public bool AddVivienda(Vivienda V)
        {
            if (V != null && V.Validar())
            {
                using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
                {
                    try
                    {
                        db.Barrios.Attach(V.Barrio);
                        db.Viviendas.Add(V);
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

        public IEnumerable<Vivienda> FindAllVivienda()
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                return db.Viviendas.ToList();
            }
        }

        public Vivienda FindByIdVivienda(int Id)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Find(Id);
                return result;
            }
        }

        public IEnumerable<Vivienda> FindByIdBarrioYHabilitada(int idBarrio)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(c => c.Barrio.Id == idBarrio && c.Estado == "Habilitada").ToList();
                return result;
            }
        }

        public IEnumerable<Vivienda> FiltrarDormitorios(int cant)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(v => v.CantDorm == cant).ToList();
                return result;
            }
        }

        public IEnumerable<Vivienda> FiltrarEstado(string estado)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(v => v.Estado == estado).ToList();
                return result;
            }
        }

        public IEnumerable<Vivienda> FiltrarTipo(string tipo)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(v => v.Tipo == tipo).ToList();
                return result;
            }
        }

        public IEnumerable<Vivienda> FiltrarRangoPrecio(double min, double max)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(v => v.PrecioFinal >= min && v.PrecioFinal <= max).ToList();
                return result;
            }
        }

        public IEnumerable<Vivienda> FiltrarBarrio(int id)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(v => v.BarrioId == id).ToList();
                return result;
            }
        }

        public IEnumerable<Vivienda> FindViviendasSinSortear()
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(v => v.Estado != "Sorteada").ToList();
                return result;
            }
        }

        public double precioFinal(int id)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.Where(v => v.ID == id).FirstOrDefault().PrecioFinal;
                return result;
            }
        }

        public bool UpdateVivienda(int id, string nuevoEstado)
        {
            using (Context.AdjudicacionContext db = new Context.AdjudicacionContext())
            {
                var result = db.Viviendas.SingleOrDefault(v => v.ID == id);
                if (result != null)
                {
                    result.Estado = nuevoEstado;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}
