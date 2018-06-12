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

        public static int addUsuario(Usuario usuario)
        {
            return Persistencia.UsuarioDD.addUsuario(usuario);
        }

        public static Usuario getUsuarioById(int idUsuario)
        {
            return Persistencia.UsuarioDD.getUsuarioById(idUsuario);
        }

        public static List<Usuario> listAllUsuarioAddAtivVincExecutor(int idUsuarioTarget)
        {
            return Persistencia.UsuarioDD.listAllUsuarioAddAtivVincExecutor(idUsuarioTarget);
        }
    }
}
