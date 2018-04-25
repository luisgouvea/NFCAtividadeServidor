using Persistencia.Modelos;
using System;
using LibraryDB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Persistencia
{
    public class TarefaCheckDD
    {
        public static bool addRegistroCheckNFC(TarefaCheck tarefaCheck)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO TarefaCheck " +
                    "(id_tarefa, nome_tarefa, data_execucao) " +
                    "VALUES (@id_tarefa, @nome_tarefa, @data_execucao)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa", tarefaCheck.IdTarefa, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@nome_tarefa", tarefaCheck.NomeTarefa, tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@data_execucao", tarefaCheck.DataExecucao, tipoDadoBD.DateTime);
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
                throw new Exception("[TarefaCheckDD.addRegistroCheckNFC()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static List<TarefaCheck> getHistoricoCheckNFCByIdsTarefa(List<string> listaIdsTarefaSearch)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {
                string sql = null;
                if (listaIdsTarefaSearch.Count() > 1)
                {
                    sql = string.Format("select * from TarefaCheck where id_tarefa in ({0})", string.Join(",", listaIdsTarefaSearch));
                }
                else
                {
                    sql = string.Format("select * from TarefaCheck where id_tarefa = {0}", listaIdsTarefaSearch[0]);
                }

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);
                
                conexao.Open();
                dReader = command.ExecuteReader();
            
                if (dReader != null)
                {
                    try
                    {
                        List<TarefaCheck> listaHistoricoCheck = new List<TarefaCheck>();
                        while (dReader.Read())
                        {
                            TarefaCheck tarefaCheck = new TarefaCheck();
                            tarefaCheck.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            tarefaCheck.NomeTarefa = Conversao.FieldToString(dReader["nome_tarefa"]);
                            listaHistoricoCheck.Add(tarefaCheck);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listaHistoricoCheck;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[TarefaCheckDD.getHistoricoCheckNFCByIdsTarefa()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }
    }
}
