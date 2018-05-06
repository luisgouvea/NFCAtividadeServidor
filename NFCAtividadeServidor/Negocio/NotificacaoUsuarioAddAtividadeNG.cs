using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NotificacaoUsuarioAddAtividadeNG
    {
        public static Boolean addNotificacaoAddAtividade(NotificacaoUsuarioAddAtividade notificacaoAddAtividade)
        {
            NotificacaoUsuario notificacao = new NotificacaoUsuario();
            notificacao.IdUsuarioNotificado = notificacaoAddAtividade.IdUsuarioNotificado;
            notificacao.DescricaoNotificacao = notificacaoAddAtividade.DescricaoNotificacao;
            notificacao.Visualizada = notificacaoAddAtividade.Visualizada;
            bool adicionado = NotificacaoUsuarioNG.addNotificacao(notificacao);

            return Persistencia.NotificacaoUsuarioAddAtividadeDD.addNotificacaoAddAtividade(notificacaoAddAtividade);
        }

        public static List<NotificacaoUsuarioAddAtividade> getNotificacoesAddAtividadeByUsuario(int idUsuario)
        {
            return Persistencia.NotificacaoUsuarioAddAtividadeDD.getNotificacoesAddAtividadeByUsuario(idUsuario);
        }
    }
}
