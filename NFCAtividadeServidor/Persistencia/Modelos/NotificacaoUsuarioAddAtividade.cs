using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class NotificacaoUsuarioAddAtividade : NotificacaoUsuario
    {
        public int IdNotificacaoUsuarioAddAtividade { get; set; }
        public int IdAtividade { get; set; }
    }
}
