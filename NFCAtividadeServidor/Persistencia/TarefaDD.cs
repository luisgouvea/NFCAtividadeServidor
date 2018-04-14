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
                            tarefa.PalavraChave = Conversao.FieldToString(dReader["palavra_chave"]);
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

        public static Tarefa getTarefaByTag(int idTag)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tarefa where id_tag = " + Convert.ToString(idTag);

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
                    "(id_atividade, id_tag, comentario) " +
                    "VALUES (@id_atividade, @id_tag, @comentario)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", tarefa.IdAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", tarefa.IdTag, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@comentario", tarefa.Nome, tipoDadoBD.VarChar);
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

        public static List<Tarefa> getTarefasAntecessoras(int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tarefa where id_tarefa in " +
                    "(" +
                        "select te.id_tarefa_antecessora from Tarefa t " +
                        "inner join TarefaEncadeamento te " +
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
                        List<Tarefa> listAntecessoras = new List<Tarefa>();
                        while (dReader.Read())
                        {
                            int idPk = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            string nome = Conversao.FieldToString(dReader["nome"]);
                            int idAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
                            Tarefa tarefa = new Tarefa();
                            tarefa.Id = idPk;
                            tarefa.Nome = nome;
                            tarefa.IdAtividade = idAtividade;
                            listAntecessoras.Add(tarefa);
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
                
        public static Boolean deleteEncadeamentoTarefa(int id_tarefa_target)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "DELETE FROM TarefaEncadeamento " +
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
                throw new Exception("[TarefaDD.deleteEncadeamentoTarefa()]: " + exp.Message);
            }
        }

        public static Boolean insertEncadeamentoTarefa(int id_tarefa_target, int id_tarefa_antecessora)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO TarefaEncadeamento " +
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
                throw new Exception("[TarefaDD.insertEncadeamentoTarefa()]: " + exp.Message);
            }
        }        
    }
}
