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
        //public static bool addRegistroCheckNFC(TarefaCheck tarefaCheck)
        //{
        //    IDbConnection conexao = null;
        //    IDbTransaction transacao = null;

        //    try
        //    {

        //        string sql = "INSERT INTO TarefaCheck " +
        //            "(id_tarefa, data_execucao) " +
        //            "VALUES (@id_tarefa, @data_execucao)";

        //        conexao = DataBase.getConection();
        //        IDbCommand command = DataBase.getCommand(sql, conexao);

        //        IDbDataParameter parametro = command.CreateParameter();
        //        DataBase.getParametroCampo(ref parametro, "@id_tarefa", tarefaCheck.IdTarefa, tipoDadoBD.Integer);
        //        command.Parameters.Add(parametro);

        //        parametro = command.CreateParameter();
        //        DataBase.getParametroCampo(ref parametro, "@data_execucao", tarefaCheck.DataExecucao, tipoDadoBD.DateTime);
        //        command.Parameters.Add(parametro);

        //        conexao.Open();
        //        transacao = conexao.BeginTransaction();
        //        command.Transaction = transacao;

        //        command.ExecuteNonQuery();

        //        if (transacao != null) transacao.Commit();
        //        if (transacao != null) transacao.Dispose();
        //        if (conexao != null) conexao.Close();

        //        return true;
        //    }
        //    catch (Exception exp)
        //    {
        //        throw new Exception("[TarefaCheckDD.addRegistroCheckNFC()]: " + exp.Message);
        //    }
        //}
    }
}
