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
        /**
         *  Sempre assumir que a lista de precedente do param tarefa (Tarefa), esta com todos os seus antecessores
        **/
        public static Boolean setarSucessaoTarefa(Tarefa tarefa)
        {
            int idTarefaTarget = tarefa.Id;
            Persistencia.TarefaSucedenteDD.deleteSucessaoTarefa(idTarefaTarget); // delete all encadeamento sucessor
            foreach (TarefaSucedente tarefaAnte in tarefa.listaSucessores)
            {
                //TODO: PEGAR O tarefaAnte e a tarefa target e inserir na tabela
            }
            return true;
        }
    }
}
