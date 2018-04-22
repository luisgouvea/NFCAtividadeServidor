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
                List<TarefaPrecedente> listAntecessoras = Persistencia.TarefaPrecedenteDD.getTarefasAntecessoras(id);
                tarefa.listaAntecessoras = listAntecessoras;
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
    }
}
