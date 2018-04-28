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
    public class TarefaPrecedenteDD
    {
        public static List<TarefaPrecedente> getTarefasAntecessoras(int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tarefa where id_tarefa in " +
                    "(" +
                        "select te.id_tarefa_antecessora from Tarefa t " +
                        "inner join TarefaPrecedente te " +
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
                        List<TarefaPrecedente> listAntecessoras = new List<TarefaPrecedente>();
                        while (dReader.Read())
                        {
                            int idPk = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            string nome = Conversao.FieldToString(dReader["nome"]);
                            int idAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
                            Tarefa tarefa = new Tarefa();
                            tarefa.Id = idPk;
                            tarefa.Nome = nome;
                            tarefa.IdAtividade = idAtividade;
                            TarefaPrecedente tarefaPrec = new TarefaPrecedente { Id = idPk, Nome = nome, IdAtividade = idAtividade };
                            listAntecessoras.Add(tarefaPrec);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listAntecessoras;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.getTarefasAntecessoras()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }


        public static List<TarefaPrecedente> getTarefasAntecessorasCheck(int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT * FROM TarefaPrecedente where id_tarefa_target = " + idTarefa;

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<TarefaPrecedente> listAntecessoras = new List<TarefaPrecedente>();
                        while (dReader.Read())
                        {
                            int idPk = Conversao.FieldToInteger(dReader["id_tarefa_precedente"]);
                            int idTarefaTarget = Conversao.FieldToInteger(dReader["id_tarefa_target"]);
                            int idTarefaAntecessora = Conversao.FieldToInteger(dReader["id_tarefa_antecessora"]);
                            TarefaPrecedente tarefaPrec = new TarefaPrecedente { Id = idTarefaTarget, IdTarefaPrecedente = idPk, IdTarefaAntecessora = idTarefaAntecessora };
                            listAntecessoras.Add(tarefaPrec);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listAntecessoras;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.getTarefasAntecessorasCheck()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

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

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.deletePrecedenciaTarefa()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
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

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaPrecedenteDD.insertPrecedenciaTarefa()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }
    }
}
