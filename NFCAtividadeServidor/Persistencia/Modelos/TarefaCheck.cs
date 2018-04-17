using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class TarefaCheck
    {
        public int Id { get; set; }
        public int IdTarefa { get; set; }
        public DateTime DataExecucao { get; set; }
        public string NomeTarefa { get; set; }
    }
}
