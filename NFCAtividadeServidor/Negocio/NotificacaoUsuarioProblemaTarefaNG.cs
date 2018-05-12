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
            
            NotificacaoUsuarioProblemaTarefa notificacaoInstance = getNewInstanceNotificacaoProblemaTarefa(notificacaoProblemaTarefa.IdTarefa);
            
            int idAdicionado = addNotificaoUsuario(notificacaoInstance);
            
            notificacaoInstance.IdNotificacaoUsuario = idAdicionado;
            notificacaoInstance.IdTarefa = notificacaoProblemaTarefa.IdTarefa;
            notificacaoInstance.DescricaoProblema = notificacaoProblemaTarefa.DescricaoProblema;
            notificacaoInstance.CheckRealizado = notificacaoProblemaTarefa.CheckRealizado;

            return Persistencia.NotificacaoUsuarioProblemaTarefaDD.addNotificacaoProblemaTarefa(notificacaoInstance);
        }

        private static int addNotificaoUsuario(NotificacaoUsuarioProblemaTarefa notificacaoInstance)
        {
            NotificacaoUsuario notificacao = new NotificacaoUsuario();
            notificacao.IdUsuarioNotificado = notificacaoInstance.IdUsuarioNotificado;
            notificacao.DescricaoNotificacao = notificacaoInstance.DescricaoNotificacao;
            notificacao.Visualizada = notificacaoInstance.Visualizada;

            int idAdicionado = NotificacaoUsuarioNG.addNotificacao(notificacao);
            return idAdicionado;
        }

        private static NotificacaoUsuarioProblemaTarefa getNewInstanceNotificacaoProblemaTarefa(int idTarefa)
        {
            Tarefa tarefa = TarefaNG.getTarefa(idTarefa);
            Atividade atividade = AtividadeNG.getAtividadeByIdAtividade(tarefa.IdAtividade);

            Usuario usuarioExecutor = UsuarioNG.getUsuarioById(atividade.IdUsuarioExecutor);

            Usuario usuarioCriador = UsuarioNG.getUsuarioById(atividade.IdUsuarioCriador);

            NotificacaoUsuarioProblemaTarefa notificacaoInstance = NotificacaoUsuarioProblemaTarefa.newInstance(usuarioExecutor.Nome, tarefa.Nome, atividade.Nome, usuarioCriador.IdUsuario, tarefa.IdTarefa);

            return notificacaoInstance;
        }

        public static List<NotificacaoUsuarioProblemaTarefa> getNotificacoesProblemaTarefaByUsuario(int idUsuario)
        {
            return Persistencia.NotificacaoUsuarioProblemaTarefaDD.getNotificacoesProblemaTarefaByUsuario(idUsuario);
        }
    }
}
