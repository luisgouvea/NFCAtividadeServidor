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
    public class TarefaDD
    {
        public static List<Tarefa> getTarefasByAtividade(int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tarefa where id_atividade = " + Convert.ToString(idAtividade);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<Tarefa> listTarefas = new List<Tarefa>();
                        while (dReader.Read())
                        {
                            Tarefa tarefa = new Tarefa();
                            tarefa.Nome = Conversao.FieldToString(dReader["nome"]);
                            tarefa.Id = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            listTarefas.Add(tarefa);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listTarefas;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaDD.getTarefasByAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static Tarefa getTarefaByTagAndTarefa(int idTag, int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tarefa where id_tag = " + Convert.ToString(idTag) + " AND" + " id_tarefa = " + Convert.ToString(idTarefa);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    dReader.Read();
                    Tarefa tarefa = new Tarefa();
                    tarefa.Nome = Conversao.FieldToString(dReader["nome"]);
                    tarefa.Id = Conversao.FieldToInteger(dReader["id_tarefa"]);
                    tarefa.IdAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);

                    conexao.Close();
                    dReader.Close();
                    return tarefa;
                }
                else
                {
                    throw new Exception("[TarefaDD.getTarefaByTag()]: Não foi possível localizar a Tag.");
                }


            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaDD.getTarefaByTag()]: " + exp.Message);
            }

            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        public static Boolean addTarefa(Tarefa tarefa)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO Tarefa " +
                    "(id_atividade, id_tag, nome) " +
                    "VALUES (@id_atividade, @id_tag, @nome)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", tarefa.IdAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tag", tarefa.IdTag, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                //parametro = command.CreateParameter();
                //DataBase.getParametroCampo(ref parametro, "@comentario", tarefa.Nome, tipoDadoBD.VarChar);
                //command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@nome", tarefa.Nome, tipoDadoBD.VarChar);
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
                throw new Exception("[TarefaDD.addTarefa()]: " + exp.Message);
            }
        }

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
                throw new Exception("[TarefaDD.getTarefasAntecessoras()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static Tarefa getTarefa(int id_tarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT * FROM Tarefa " +
                    "WHERE id_tarefa = @id_tarefa";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa", id_tarefa, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    dReader.Read();
                    Tarefa tarefa = new Tarefa();
                    tarefa.IdAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
                    tarefa.Nome = Conversao.FieldToString(dReader["nome"]);
                    tarefa.Id = Conversao.FieldToInteger(dReader["id_tarefa"]);
                    tarefa.IdTag = Conversao.FieldToInteger(dReader["id_tag"]);

                    conexao.Close();
                    dReader.Close();
                    return tarefa;
                }
                else
                {
                    throw new Exception("[TarefaDD.getTarefa()]: Não foi possível localizar a Tarefa.");
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaDD.getTarefa()]: " + exp.Message);
            }
        }
    }
}
