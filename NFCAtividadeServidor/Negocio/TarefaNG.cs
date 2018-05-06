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
            try
            {
                tarefa.IdStatusExecucao = 1;
                return Persistencia.TarefaDD.addTarefa(tarefa);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unique_TarefaNome"))
                {
                    throw new Exception("Você já possui uma Tarefa com esse nome. Informe outro nome!");
                }
                else
                {
                    throw ex;
                }
            }
        }

        public static List<Tarefa> getAllTarefasByIdAtividade(int idAtividade)
        {
            List<Tarefa> listTarefas = Persistencia.TarefaDD.getTarefasByAtividade(idAtividade);
            foreach (Tarefa tarefa in listTarefas)
            {
                int id = tarefa.IdTarefa;
                List<TarefaPrecedente> listAntecessoras = Persistencia.TarefaPrecedenteDD.getTarefasAntecessorasCheck(id);
                tarefa.listaAntecessoras = listAntecessoras;
            }
            return listTarefas;
        }

        public static Tarefa getTarefaByTagAndTarefa(string identificadorTag, int idTarefa)
        {
            Tarefa tarefa = Persistencia.TarefaDD.getTarefaByTagAndTarefa(identificadorTag, idTarefa);
            return tarefa;
        }

        public static Tarefa getTarefa(int idTarefa)
        {
            Tarefa tarefa = Persistencia.TarefaDD.getTarefa(idTarefa);
            return tarefa;
        }

        public static void updateStatusExecucao(int idStatusExecucao, int idTarefa)
        {
            Persistencia.TarefaDD.updateStatusExecucao(idStatusExecucao, idTarefa);
        }

        public static void updateStatusExecucaoByIdAtividade(int idStatusExecucao, int idAtividade)
        {
            Persistencia.TarefaDD.updateStatusExecucaoByIdAtividade(idStatusExecucao, idAtividade);
        }
    }
}
