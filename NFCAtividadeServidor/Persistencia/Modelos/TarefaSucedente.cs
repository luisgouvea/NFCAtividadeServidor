using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class TarefaSucedente : Tarefa
    {
        //public int IdTarefaTarget { get; set; }
        public int IdTarefaSucedente { get; set; } // PK
        public int IdTarefaProxima { get; set; }
    }
}
