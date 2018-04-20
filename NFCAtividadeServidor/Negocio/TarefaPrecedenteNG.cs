using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarefaPrecedenteNG
    {
        /**
         *  Sempre assumir que a lista de precedente do param tarefa (Tarefa), esta com todos os seus antecessores
        **/
        public static Boolean setarPrecedenciaTarefa(Tarefa tarefa)
        {
            int idTarefaTarget = tarefa.Id;
            Persistencia.TarefaPrecedenteDD.deleteSucessaoTarefa(idTarefaTarget); // delete all encadeamento sucessor
            Persistencia.TarefaPrecedenteDD.deletePrecedenciaTarefa(idTarefaTarget); // delete all encadeamento precedente
            foreach (TarefaPrecedente tarefaAnte in tarefa.listaAntecessores)
            {
                //TODO: PEGAR O tarefaAnte e a tarefa target e inserir na tabela
                Persistencia.TarefaPrecedenteDD.insertPrecedenciaTarefa(idTarefaTarget, tarefaAnte.Id); // cria novamente
            }
            return true;
        }
    }
}
