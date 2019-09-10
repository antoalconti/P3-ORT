using Dominio.Context;
using Dominio.EntidadesDeNegocio;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        public Usuario FindUsuario(string DatoLogin, string PasswordLogin)
        {
            try
            {
                using (AdjudicacionContext db = new AdjudicacionContext())
                {
                    if (!DatoLogin.Contains("@"))
                    {
                        DatoLogin = db.Postulantes.Where(p => p.Cedula == DatoLogin).Select(p => p.Usuario.Mail).FirstOrDefault();
                    }
                    var usr = db.Usuarios.SingleOrDefault
                        (u => u.Mail.ToUpper() == DatoLogin.ToUpper()
                        && u.Contra == PasswordLogin);
                    return usr;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
