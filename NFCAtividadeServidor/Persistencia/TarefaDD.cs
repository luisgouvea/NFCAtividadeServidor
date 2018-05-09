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
                            Tarefa tarefa = getDadosTarefa(dReader);
                            listTarefas.Add(tarefa);
                        }
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

        public static bool updateStatusExecucaoByIdAtividade(int idStatusExecucao, int idAtividade)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "UPDATE Tarefa " +
                    "SET id_status_execucao = @id_status_execucao " +
                    "WHERE id_atividade = @id_atividade";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_status_execucao", idStatusExecucao, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", idAtividade, tipoDadoBD.Integer);
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
                throw new Exception("[TarefaDD.updateStatusExecucaoByIdAtividade()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static bool updateStatusExecucao(int idStatusExecucao, int idTarefa)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "UPDATE Tarefa " +
                    "SET id_status_execucao = @id_status_execucao " +
                    "WHERE id_tarefa = @id_tarefa";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa", idTarefa, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_status_execucao", idStatusExecucao, tipoDadoBD.Integer);
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
                throw new Exception("[TarefaDD.updateStatusExecucao()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static Tarefa getTarefaByTagAndTarefa(int identificadorTag, int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tarefa where identificador_tag = " + Convert.ToString(identificadorTag) + " AND" + " id_tarefa = " + Convert.ToString(idTarefa);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    dReader.Read();
                    Tarefa tarefa = getDadosTarefa(dReader);
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
                    "(id_atividade, identificador_tag, nome, inicia_fluxo, finaliza_fluxo, id_status_execucao) " +
                    "VALUES (@id_atividade, @identificador_tag, @nome, @inicia_fluxo, @finaliza_fluxo, @id_status_execucao)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", tarefa.IdAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@identificador_tag", tarefa.IdentificadorTag, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_status_execucao", tarefa.IdStatusExecucao, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                //parametro = command.CreateParameter();
                //DataBase.getParametroCampo(ref parametro, "@comentario", tarefa.Nome, tipoDadoBD.VarChar);
                //command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@nome", tarefa.Nome, tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@inicia_fluxo", tarefa.IniciaFluxo, tipoDadoBD.Boolean);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@finaliza_fluxo", tarefa.FinalizaFluxo, tipoDadoBD.Boolean);
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
                throw new Exception("[TarefaDD.addTarefa()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
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
                    Tarefa tarefa = getDadosTarefa(dReader);
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
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        private static Tarefa getDadosTarefa(IDataReader dReader)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Nome = Conversao.FieldToString(dReader["nome"]);
            tarefa.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
            tarefa.IdAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
            tarefa.IdentificadorTag = Conversao.FieldToInteger(dReader["identificador_tag"]);
            tarefa.IdStatusExecucao = Conversao.FieldToInteger(dReader["id_status_execucao"]);
            tarefa.IniciaFluxo = Conversao.FieldToBoolean(dReader["inicia_fluxo"]);
            tarefa.FinalizaFluxo = Conversao.FieldToBoolean(dReader["finaliza_fluxo"]);
            return tarefa;
        }
    }
}
