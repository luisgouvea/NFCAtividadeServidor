﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class TarefaCheck : Tarefa
    {
        //public int Id { get; set; }
        public int IdTarefaCheck { get; set; } // PK da tabela TarefaCheck
        public int IdStatusCheckNFC { get; set; } 
        public DateTime DataExecucao { get; set; }
        public int Ciclo { get; set; }
        //public string NomeTarefa { get; set; }
    }
}
