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

        /**
         *  Sempre assumir que a lista de precedencia do param tarefa (Tarefa), esta com todos os seus antecessores
        **/
        public static Boolean setarSucessaoTarefa(Tarefa tarefa)
        {
            //idTarefaSucessao
            int idTarefaSucessao = tarefa.Id;
            Persistencia.TarefaSucedenteDD.deleteSucessaoTarefa(idTarefaSucessao); // delete all encadeamento sucessor
            foreach (TarefaSucedente tarefaSucessao in tarefa.listaSucessores)
            {
                //TODO: PEGAR O tarefaAnte e a tarefa target e inserir na tabela
                Persistencia.TarefaSucedenteDD.insertSucessaoTarefa(tarefaSucessao.Id, idTarefaSucessao); // cria novamente
            }
            return true;
        }
    }
}
