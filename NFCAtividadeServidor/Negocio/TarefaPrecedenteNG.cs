﻿using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarefaPrecedenteNG
    {

        public static List<TarefaPrecedente> getTarefasAntecessoras(int idTarefa)
        {
            return Persistencia.TarefaPrecedenteDD.getTarefasAntecessoras(idTarefa);
        }

        /**
         *  Sempre assumir que a lista de precedente do param tarefa (Tarefa), esta com todos os seus antecessores
        **/
        public static Boolean setarPrecedenciaTarefa(Tarefa tarefa)
        {
            int idTarefaTarget = tarefa.Id;           
            Persistencia.TarefaPrecedenteDD.deletePrecedenciaTarefa(idTarefaTarget); // delete all encadeamento precedente
            foreach (TarefaPrecedente tarefaAnte in tarefa.listaAntecessoras)
            {
                Persistencia.TarefaPrecedenteDD.insertPrecedenciaTarefa(idTarefaTarget, tarefaAnte.IdTarefaAntecessora); // cria novamente
            }
            return true;
        }
    }
}