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
    public class NotificacaoUsuarioDD
    {
        public static int addNotificacao(NotificacaoUsuario notificacao)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "INSERT INTO NotificacaoUsuario " +
                    "(id_usuario_notificado, descricao_notificacao, visualizada, data_notificacao) " +
                    "VALUES (@id_usuario_notificado, @descricao_notificacao, @visualizada, @data_notificacao); SELECT SCOPE_IDENTITY();";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                ExecuteAddAndUpdate(notificacao, command);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                //command.ExecuteNonQuery();
                int lastId = Convert.ToInt32(command.ExecuteScalar());

                if (transacao != null) transacao.Commit();

                return lastId;
            }
            catch (Exception exp)
            {
                throw new Exception("[NotificacaoUsuarioDD.addNotificacao()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static int getCountNotificacoesParaVisualizarUsuario(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT * FROM  NotificacaoUsuario " +
                    "WHERE id_usuario_notificado = @id_usuario_notificado " +
                    "AND visualizada = 0";

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
                        int count = 0;
                        while (dReader.Read())
                        {
                            count += 1;
                        }
                        return count;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[NotificacaoUsuarioDD.getCountNotificacoesParaVisualizarUsuario()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return 0;
        }

        public static bool updateNotificacao(NotificacaoUsuario notificacao)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "UPDATE NotificacaoUsuario " +
                    "SET id_usuario_notificado = @id_usuario_notificado, " +
                    "descricao_notificacao = @descricao_notificacao, " +
                    "visualizada = @visualizada, " +
                    "data_notificacao = @data_notificacao " +
                    "WHERE id_notificacao_usuario = @id_notificacao_usuario";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = ExecuteAddAndUpdate(notificacao, command);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_notificacao_usuario", notificacao.IdNotificacaoUsuario, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();


                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[NotificacaoUsuarioDD.updateNotificacao()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static List<NotificacaoUsuario> getNotificacoesByUsuario(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {
                string sql = "select * from NotificacaoUsuario where id_usuario_notificado = @id_usuario_notificado";

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
                        List<NotificacaoUsuario> listNotificacao = new List<NotificacaoUsuario>();
                        while (dReader.Read())
                        {
                            NotificacaoUsuario noti = getDadosNotificacao(dReader);
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
                throw new Exception("[NotificacaoUsuarioDD.getNotificacoesByUsuario()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        private static IDbDataParameter ExecuteAddAndUpdate(NotificacaoUsuario notificacao, IDbCommand command)
        {
            IDbDataParameter parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_usuario_notificado", notificacao.IdUsuarioNotificado, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@descricao_notificacao", notificacao.DescricaoNotificacao, tipoDadoBD.VarChar);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@visualizada", notificacao.Visualizada, tipoDadoBD.Boolean);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@data_notificacao", Conversao.DateTimeToField(notificacao.DataNotificacao), tipoDadoBD.DateTime);
            command.Parameters.Add(parametro);

            return parametro;
        }

        private static NotificacaoUsuario getDadosNotificacao(IDataReader dReader)
        {
            NotificacaoUsuario notificacao = new NotificacaoUsuario();
            notificacao.IdNotificacaoUsuario = Conversao.FieldToInteger(dReader["id_notificacao_usuario"]);
            notificacao.IdUsuarioNotificado = Conversao.FieldToInteger(dReader["id_usuario_notificado"]);
            notificacao.DescricaoNotificacao = Conversao.FieldToString(dReader["descricao_notificacao"]);
            notificacao.DataNotificacao = Conversao.FieldToDateTime(dReader["data_notificacao"]);
            notificacao.Visualizada = Conversao.FieldToBoolean(dReader["visualizada"]);
            return notificacao;
        }
    }
}
