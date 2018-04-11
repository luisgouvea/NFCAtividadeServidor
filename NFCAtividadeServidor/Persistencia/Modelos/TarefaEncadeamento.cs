using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia.Modelos
{
    public class TarefaEncadeamento
    {
        public int Id { get; set; }
        public int IdTarefaTarget { get; set; }
        public int IdTarefaAntecessora { get; set; }
    }
}
