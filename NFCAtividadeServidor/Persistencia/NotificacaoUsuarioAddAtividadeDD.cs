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
    public class NotificacaoUsuarioAddAtividadeDD
    {
        public static bool addNotificacaoAddAtividade(NotificacaoUsuarioAddAtividade notificacaoAddAtividade)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "INSERT INTO NotificacaoUsuarioAddAtividade " +
                    "(id_notificacao_usuario, id_atividade) " +
                    "VALUES (@id_notificacao_usuario, @id_atividade)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                ExecuteAddAndUpdate(notificacaoAddAtividade, command);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();


                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[NotificacaoUsuarioAddAtividadeDD.addNotificacaoAddAtividade()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static List<NotificacaoUsuarioAddAtividade> getNotificacoesAddAtividadeByUsuario(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {
                string sql = "SELECT nu.id_notificacao_usuario, nu.descricao_notificacao, nu.visualizada, nu.id_usuario_notificado, nuaddAtiv.id_atividade FROM NotificacaoUsuario nu " +
                    "INNER JOIN NotificacaoUsuarioAddAtividade nuaddAtiv " +
                    "ON nu.id_notificacao_usuario = nuaddAtiv.id_notificacao_usuario " +
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
                        List<NotificacaoUsuarioAddAtividade> listNotificacao = new List<NotificacaoUsuarioAddAtividade>();
                        while (dReader.Read())
                        {
                            NotificacaoUsuarioAddAtividade noti = getDadosNotificacaoAddAtividade(dReader);
                            listNotificacao.Add(noti);
                        }

                        return listNotificacao;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[NotificacaoUsuarioDD.getNotificacoesAddAtividadeByUsuario()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        private static NotificacaoUsuarioAddAtividade getDadosNotificacaoAddAtividade(IDataReader dReader)
        {
            NotificacaoUsuarioAddAtividade notificacao = new NotificacaoUsuarioAddAtividade();
            notificacao.IdNotificacaoUsuario = Conversao.FieldToInteger(dReader["id_notificacao_usuario"]);
            notificacao.IdUsuarioNotificado = Conversao.FieldToInteger(dReader["id_usuario_notificado"]);
            notificacao.IdAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
            notificacao.DescricaoNotificacao = Conversao.FieldToString(dReader["descricao_notificacao"]);
            notificacao.Visualizada = Conversao.FieldToBoolean(dReader["visualizada"]);
            return notificacao;
        }

        private static IDbDataParameter ExecuteAddAndUpdate(NotificacaoUsuarioAddAtividade notificacaoAddAtividade, IDbCommand command)
        {
            IDbDataParameter parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_notificacao_usuario", notificacaoAddAtividade.IdNotificacaoUsuario, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_atividade", notificacaoAddAtividade.IdAtividade, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);           

            return parametro;
        }
    }
}
