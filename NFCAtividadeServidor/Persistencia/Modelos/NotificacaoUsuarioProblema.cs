using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class NotificacaoUsuarioProblemaTarefa : NotificacaoUsuario
    {
        public int IdNotificacaoUsuarioProblemaTarefa { get; set; }
        public int IdTarefa { get; set; }
    }
}
