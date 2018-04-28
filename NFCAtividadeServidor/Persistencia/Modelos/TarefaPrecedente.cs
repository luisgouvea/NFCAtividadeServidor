using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia.Modelos
{
    public class TarefaPrecedente : Tarefa
    {
        //public int Id { get; set; }
        //public int IdTarefaTarget { get; set; }
        public int IdTarefaPrecedente { get; set; } // PK da tabela
        public int IdTarefaAntecessora { get; set; }
    }
}
