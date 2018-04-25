using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarefaSucedenteNG
    {
        public static List<TarefaSucedente> getTarefasSucessoras(int idTarefa)
        {
            return Persistencia.TarefaSucedenteDD.getTarefasSucessoras(idTarefa);
        }

        public static List<TarefaSucedente> getTarefasSucessorasCheck(int idTarefa)
        {
            return Persistencia.TarefaSucedenteDD.getTarefasSucessorasCheck(idTarefa);
        }

        /**
         *  Sempre assumir que a lista de precedencia do param tarefa (Tarefa), esta com todos os seus antecessores
        **/
        public static Boolean setarSucessaoTarefa(Tarefa tarefa)
        {
            //id_tarefa_proxima
            int idTarefaSucessao = tarefa.Id;
            //Persistencia.TarefaSucedenteDD.deleteSucessaoTarefa(idTarefaSucessao); // delete all encadeamento sucessor
            foreach (TarefaPrecedente tarefaSucessao in tarefa.listaAntecessoras)
            {
                Persistencia.TarefaSucedenteDD.deleteSucessaoTarefa(tarefaSucessao.IdTarefaAntecessora); // delete all encadeamento sucessor
                Persistencia.TarefaSucedenteDD.insertSucessaoTarefa(tarefaSucessao.IdTarefaAntecessora, idTarefaSucessao); // cria novamente
            }
            return true;
        }
    }
}
