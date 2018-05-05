using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class NotificacaoUsuario
    {
        public int IdNotificacaoUsuario { get; set; }
        public int IdUsuario { get; set; }
        public string DescricaoNotificacao { get; set; }
        public bool Visualizada { get; set; }
    }
}
