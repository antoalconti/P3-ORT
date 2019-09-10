using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dominio.EntidadesDeNegocio;

namespace Dominio.Context
{
    public class AdjudicacionContext: DbContext
    {
        public DbSet<Sorteo>Sorteos{ get; set; }
        public DbSet<Postulante> Postulantes { get; set; }
        public DbSet<Vivienda> Viviendas { get; set; }
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<Barrio> Barrios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        public AdjudicacionContext() : base("mi") { }
    }
}
