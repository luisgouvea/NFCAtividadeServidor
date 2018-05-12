using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class NotificacaoUsuarioProblemaTarefa : NotificacaoUsuario
    {
        public int IdNotificacaoUsuarioProblemaTarefa { get; set; }
        public int IdTarefa { get; set; }
        public string DescricaoProblema { get; set; }
        public bool CheckRealizado { get; set; }


        public static NotificacaoUsuarioProblemaTarefa newInstance(string nomeExecutor, string nomeTarefa, string nomeAtividade, int IdUsuarioCriador, int idTarefaProblema)
        {
            NotificacaoUsuarioProblemaTarefa noti = new NotificacaoUsuarioProblemaTarefa();

            noti.DescricaoNotificacao = "O usuário <b>" + nomeExecutor + "</b> sinalizou um problema com a tarefa <b>" + nomeTarefa + "</b>, da atividade " + nomeAtividade;
            noti.Visualizada = false;
            noti.IdUsuarioNotificado = IdUsuarioCriador;
            noti.IdTarefa = idTarefaProblema;
            noti.DataNotificacao = DateTime.Now;

            return noti;
        }
    }
}
