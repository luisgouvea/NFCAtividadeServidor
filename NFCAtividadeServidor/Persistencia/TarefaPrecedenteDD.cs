using LibraryDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class TarefaPrecedenteDD
    {
        public static Boolean deletePrecedenciaTarefa(int id_tarefa_target)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "DELETE FROM TarefaPrecedente " +
                    "WHERE id_tarefa_target = @id_tarefa_target";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa_target", id_tarefa_target, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.deletePrecedenciaTarefa()]: " + exp.Message);
            }
        }

        public static Boolean deleteSucessaoTarefa(int id_tarefa_target)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "DELETE FROM TarefaSucessora " +
                    "WHERE id_tarefa_target = @id_tarefa_target";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa_target", id_tarefa_target, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.deleteSucessaoTarefa()]: " + exp.Message);
            }
        }
        
        public static Boolean insertPrecedenciaTarefa(int id_tarefa_target, int id_tarefa_antecessora)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO TarefaPrecedente " +
                    "(id_tarefa_target, id_tarefa_antecessora) " +
                    "VALUES (@id_tarefa_target, @id_tarefa_antecessora)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa_target", id_tarefa_target, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa_antecessora", id_tarefa_antecessora, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.insertPrecedenciaTarefa()]: " + exp.Message);
            }
        }

        public static Boolean insertSucessaoTarefa(int id_tarefa_target, int id_tarefa_proxima)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO TarefaSucessora " +
                    "(id_tarefa_target, id_tarefa_proxima) " +
                    "VALUES (@id_tarefa_target, @id_tarefa_proxima)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa_target", id_tarefa_target, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa_proxima", id_tarefa_proxima, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.insertSucessaoTarefa()]: " + exp.Message);
            }
        }
    }
}
