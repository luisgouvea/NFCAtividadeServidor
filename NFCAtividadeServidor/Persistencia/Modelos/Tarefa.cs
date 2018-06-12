using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class Tarefa
    {
        public int IdTarefa { get; set; }
        public int IdAtividade { get; set; }
        public int IdentificadorTag { get; set; }
        public int IdStatusExecucao { get; set; }
        public string Nome { get; set; }
        //public DateTime DataCheck { get; set; }
        public string Comentario { get; set; }
        public bool IniciaFluxo { get; set; }
        public bool FinalizaFluxo { get; set; }
        public List<TarefaPrecedente> listaAntecessoras { get; set; }
        public List<TarefaSucedente> listaSucessoras { get; set; }
    }
}
