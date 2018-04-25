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
    public class TarefaSucedenteDD
    {
        public static List<TarefaSucedente> getTarefasSucessoras(int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tarefa where id_tarefa in " +
                    "(" +
                        "select te.id_tarefa_proxima from Tarefa t " +
                        "inner join TarefaSucessora te " +
                        "on t.id_tarefa = te.id_tarefa_target " +
                        "where te.id_tarefa_target = " + idTarefa
                        +
                    ")";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<TarefaSucedente> listSucessoras = new List<TarefaSucedente>();
                        while (dReader.Read())
                        {
                            int idPk = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            string nome = Conversao.FieldToString(dReader["nome"]);
                            int idAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
                            TarefaSucedente tarefaSuced = new TarefaSucedente { Id = idPk, Nome = nome, IdAtividade = idAtividade };
                            listSucessoras.Add(tarefaSuced);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listSucessoras;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaSucedenteDD.getTarefasSucessoras()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static List<TarefaSucedente> getTarefasSucessorasCheck(int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT * FROM TarefaSucessora where id_tarefa_target = " + idTarefa;
               
                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<TarefaSucedente> listSucessoras = new List<TarefaSucedente>();
                        while (dReader.Read())
                        {
                            int idPk = Conversao.FieldToInteger(dReader["id_tarefa_sucessora"]);
                            int idTarefaTarget = Conversao.FieldToInteger(dReader["id_tarefa_target"]);
                            int idTarefaProxima = Conversao.FieldToInteger(dReader["id_tarefa_proxima"]);
                            TarefaSucedente tarefaSuced = new TarefaSucedente { Id = idPk, IdTarefaTarget = idTarefaTarget, IdTarefaProxima = idTarefaProxima};
                            listSucessoras.Add(tarefaSuced);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listSucessoras;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaSucedenteDD.getTarefasSucessorasCheck()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
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

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaSucedenteDD.deleteSucessaoTarefa()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
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

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaSucedenteDD.insertSucessaoTarefa()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }
    }
}
