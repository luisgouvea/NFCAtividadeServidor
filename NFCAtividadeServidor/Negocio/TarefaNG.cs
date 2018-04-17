using System;
using Persistencia.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarefaNG
    {
        public static Boolean addTarefa(Tarefa tarefa)
        {
            return Persistencia.TarefaDD.addTarefa(tarefa);
        }

        public static List<Tarefa> getAllTarefasByIdAtividade(int idAtividade)
        {
            List<Tarefa> listTarefas = Persistencia.TarefaDD.getTarefasByAtividade(idAtividade);
            foreach (Tarefa tarefa in listTarefas)
            {
                int id = tarefa.Id;
                List<Tarefa> listAntecessoras = Persistencia.TarefaDD.getTarefasAntecessoras(id);
                tarefa.listaEncadeamento = listAntecessoras;
            }
            return listTarefas;
        }

        public static Tarefa getTarefaByTagAndTarefa(int idTag, int idTarefa)
        {
            Tarefa tarefa = Persistencia.TarefaDD.getTarefaByTagAndTarefa(idTag, idTarefa);
            return tarefa;
        }

        public static Tarefa getTarefa(int idTarefa)
        {
            Tarefa tarefa = Persistencia.TarefaDD.getTarefa(idTarefa);
            return tarefa;
        }

        public static List<Tarefa> getTarefasAntecessoras(int idTarefa)
        {
            return Persistencia.TarefaDD.getTarefasAntecessoras(idTarefa);
        }

        /**
         *  Sempre assumir que a lista de encadeamento do param tarefa (Tarefa), esta com todos os seus antecessores
        **/
        public static Boolean setarEncadeamentoTarefa(Tarefa tarefa)
        {
            int idTarefaTarget = tarefa.Id;
            Persistencia.TarefaDD.deleteEncadeamentoTarefa(idTarefaTarget); // delete all encadeamento
            foreach (Tarefa tarefaAnte in tarefa.listaEncadeamento)
            {
                Persistencia.TarefaDD.insertEncadeamentoTarefa(idTarefaTarget, tarefaAnte.Id); // cria novamente
            }
            return true;
        }        
    }
}
