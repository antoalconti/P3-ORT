using Dominio.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepositorioUsuario
    {
        Usuario FindUsuario(string DatoLogin, string PasswordLogin);
    }
}
