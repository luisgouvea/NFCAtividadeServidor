using System;
using Persistencia.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarefaCheckNG
    {
        public static bool addRegistroCheckNFC(Tarefa tarefa)
        {
            TarefaCheck tarefaCheck = new TarefaCheck();
            tarefaCheck.IdTarefa = tarefa.Id;
            tarefaCheck.DataExecucao = DateTime.Now;
            return Persistencia.TarefaCheckDD.addRegistroCheckNFC(tarefaCheck);
        }
    }
}
