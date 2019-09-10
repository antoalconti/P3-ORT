using Dominio.EntidadesDeNegocio;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiPostulante.Controllers
{
    public class ParticipanteController : ApiController
    {
        // GET: api/Postulante
        RepositorioPostulante repo = new RepositorioPostulante();

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Postulante/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Postulante
        
        public IHttpActionResult Post([FromBody]Postulante unPostulante)
        {
            if (repo.AddPostulante(unPostulante))
            {
                return (Created("SE CREO",unPostulante));
            }
            else {
                return InternalServerError();
            }
            
        }

        // PUT: api/Postulante/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Postulante/5
        public void Delete(int id)
        {
        }
    }
}
