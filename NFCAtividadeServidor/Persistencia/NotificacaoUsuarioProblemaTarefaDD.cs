using LibraryDB;
using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class NotificacaoUsuarioProblemaTarefaDD
    {
        public static bool addNotificacaoProblemaTarefa(NotificacaoUsuarioProblemaTarefa notificacaoProblemaTarefa)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "INSERT INTO NotificacaoUsuarioProblemaTarefa " +
                    "(id_notificacao_usuario_problema_tarefa, id_tarefa) " +
                    "VALUES (@id_notificacao_usuario_problema_tarefa, @id_tarefa)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                ExecuteAddAndUpdate(notificacaoProblemaTarefa, command);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();


                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[NotificacaoUsuarioProblemaTarefaDD.addNotificacaoProblemaTarefa()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static List<NotificacaoUsuarioProblemaTarefa> getNotificacoesProblemaTarefaByUsuario(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {
                string sql = "SELECT nu.descricao_notificacao, nu.visualizada, nupt.id_tarefa FROM NotificacaoUsuario nu " +
                    "INNER JOIN NotificacaoUsuarioProblemaTarefa nupt " +
                    "ON nu.id_notificacao_usuario = nupt.id_notificacao_usuario " +
                    "WHERE nu.id_usuario_notificado = @id_usuario_notificado";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_usuario_notificado", idUsuario, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<NotificacaoUsuarioProblemaTarefa> listNotificacao = new List<NotificacaoUsuarioProblemaTarefa>();
                        while (dReader.Read())
                        {
                            NotificacaoUsuarioProblemaTarefa noti = getDadosNotificacaoProblemaTarefa(dReader);
                            listNotificacao.Add(noti);
                        }

                        return listNotificacao;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[NotificacaoUsuarioProblemaTarefaDD.getNotificacoesProblemaTarefaByUsuario()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        private static NotificacaoUsuarioProblemaTarefa getDadosNotificacaoProblemaTarefa(IDataReader dReader)
        {
            NotificacaoUsuarioProblemaTarefa notificacao = new NotificacaoUsuarioProblemaTarefa();
            notificacao.IdNotificacaoUsuario = Conversao.FieldToInteger(dReader["id_notificacao_usuario"]);
            notificacao.IdUsuarioNotificado = Conversao.FieldToInteger(dReader["id_usuario_notificado"]);
            notificacao.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
            notificacao.DescricaoNotificacao = Conversao.FieldToString(dReader["descricao_notificacao"]);
            notificacao.Visualizada = Conversao.FieldToBoolean(dReader["visualizada"]);
            return notificacao;
        }

        private static IDbDataParameter ExecuteAddAndUpdate(NotificacaoUsuarioProblemaTarefa notificacaoProblemaTarefa, IDbCommand command)
        {
            IDbDataParameter parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_notificacao_usuario", notificacaoProblemaTarefa.IdNotificacaoUsuario, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_tarefa", notificacaoProblemaTarefa.IdTarefa, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            return parametro;
        }
    }
}
