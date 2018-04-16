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
                    "(id_tarefa, id_atividade, data_execucao) " +
                    "VALUES (@id_tarefa, @id_atividade, @data_execucao)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa", tarefaCheck.IdTarefa, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", tarefaCheck.IdAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@data_execucao", tarefaCheck.DataExecucao, tipoDadoBD.DateTime);
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
                throw new Exception("[TarefaCheckDD.addRegistroCheckNFC()]: " + exp.Message);
            }
        }

        public static List<TarefaCheck> getHistoricoCheckNFCByIdAtividade(int idAtividade, int idTarefa)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from TarefaCheck" +
                    " where id_atividade = @id_atividade" +
                    " AND" +
                    " id_tarefa != @id_tarefa";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", idAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa", idTarefa, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();
            
                if (dReader != null)
                {
                    try
                    {
                        List<TarefaCheck> listaHistoricoCheck = new List<TarefaCheck>();
                        while (dReader.Read())
                        {
                            int idPk = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            TarefaCheck tarefaCheck = new TarefaCheck();
                            tarefaCheck.IdTarefa = idPk;
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
                throw new Exception("[TarefaCheckDD.getHistoricoCheckNFCByIdAtividade()]: " + exp.Message);
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
