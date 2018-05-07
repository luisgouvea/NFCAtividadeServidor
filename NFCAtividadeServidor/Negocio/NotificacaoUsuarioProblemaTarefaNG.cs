using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NotificacaoUsuarioProblemaTarefaNG
    {
        public static Boolean addNotificacaoProblemaTarefa(NotificacaoUsuarioProblemaTarefa notificacaoProblemaTarefa)
        {
            NotificacaoUsuarioProblemaTarefa notificacao = new NotificacaoUsuarioProblemaTarefa();
            notificacao.IdUsuarioNotificado = notificacaoProblemaTarefa.IdUsuarioNotificado;
            notificacao.DescricaoNotificacao = notificacaoProblemaTarefa.DescricaoNotificacao;
            notificacao.Visualizada = notificacaoProblemaTarefa.Visualizada;
            int adicionado = NotificacaoUsuarioNG.addNotificacao(notificacao);

            notificacaoProblemaTarefa.IdNotificacaoUsuario = adicionado;
            return Persistencia.NotificacaoUsuarioProblemaTarefaDD.addNotificacaoProblemaTarefa(notificacaoProblemaTarefa);
        }

        public static List<NotificacaoUsuarioProblemaTarefa> getNotificacoesProblemaTarefaByUsuario(int idUsuario)
        {
            return Persistencia.NotificacaoUsuarioProblemaTarefaDD.getNotificacoesProblemaTarefaByUsuario(idUsuario);
        }
    }
}
