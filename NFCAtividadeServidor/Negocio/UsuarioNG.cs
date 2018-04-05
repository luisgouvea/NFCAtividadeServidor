using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNG
    {
        public static Usuario getUsuario(string login, string senha)
        {
            return Persistencia.UsuarioDD.getUsuario(login, senha);
        }

        public static List<Usuario> listAllUsuarioAddAtivVincExecutor(int idUsuarioTarget)
        {
            return Persistencia.UsuarioDD.listAllUsuarioAddAtivVincExecutor(idUsuarioTarget);
        }
    }
}
