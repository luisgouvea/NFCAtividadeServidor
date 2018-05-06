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
        public static Boolean addNotificacaoProblemaTarefa(NotificacaoUsuarioProblemaTarefa notificacaoAddAtividade)
        {
            NotificacaoUsuarioProblemaTarefa notificacao = new NotificacaoUsuarioProblemaTarefa();
            notificacao.IdUsuarioNotificado = notificacaoAddAtividade.IdUsuarioNotificado;
            notificacao.DescricaoNotificacao = notificacaoAddAtividade.DescricaoNotificacao;
            notificacao.Visualizada = notificacaoAddAtividade.Visualizada;
            bool adicionado = NotificacaoUsuarioNG.addNotificacao(notificacao);

            return Persistencia.NotificacaoUsuarioProblemaTarefaDD.addNotificacaoProblemaTarefa(notificacaoAddAtividade);
        }

        public static List<NotificacaoUsuarioProblemaTarefa> getNotificacoesProblemaTarefaByUsuario(int idUsuario)
        {
            return Persistencia.NotificacaoUsuarioProblemaTarefaDD.getNotificacoesProblemaTarefaByUsuario(idUsuario);
        }
    }
}
