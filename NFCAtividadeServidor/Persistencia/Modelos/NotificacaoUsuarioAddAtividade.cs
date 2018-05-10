using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class NotificacaoUsuarioAddAtividade : NotificacaoUsuario
    {
        public int IdNotificacaoUsuarioAddAtividade { get; set; }
        public int IdAtividade { get; set; }

        public static NotificacaoUsuarioAddAtividade newInstance(string nomeCriador, string nomeAtividade, int IdUsuarioExecutor, int idAtividadeAdicionada)
        {
            NotificacaoUsuarioAddAtividade noti = new NotificacaoUsuarioAddAtividade();

            noti.DescricaoNotificacao = "O usuário <b>" + nomeCriador + "</b> criou a atividade <b>" + nomeAtividade + "</b> e vinculou você como executor.";
            noti.Visualizada = false;
            noti.IdUsuarioNotificado = IdUsuarioExecutor;
            noti.IdAtividade = idAtividadeAdicionada;
            noti.DataNotificacao = DateTime.Now;

            return noti;
        }
    }
}
