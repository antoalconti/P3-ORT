using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdjudicacionVivienda.Models
{
   public class BarrioModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        
        public string Catastro { get; set; }
        
        public string Descripcion { get; set; }
    }
}
