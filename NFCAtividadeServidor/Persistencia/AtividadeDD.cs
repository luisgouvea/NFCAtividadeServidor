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
    public class AtividadeDD
    {
        public static List<Atividade> GetAllAtividades()
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                //string sql = "SELECT [campo], [nome], [local], [descricao] FROM validarNFe WHERE campo LIKE @campo";
                string sql = "select * from Atividade;";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                //IDbDataParameter parametro = command.CreateParameter();
                //DataBase.getParametroCampo(ref parametro, "@campo", campo.Trim(), tipoDadoBD.VarChar, clienteWeb.BancoCliente.Provider, campo.Trim().Length);
                //DataBase.getParametroCampo(ref parametro, "@campo", "gg", tipoDadoBD.VarChar, "ff".Length);
                //command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<Atividade> listAtividades = new List<Atividade>();
                        while (dReader.Read())
                        {
                            Atividade atividade = new Atividade();
                            atividade.Nome = dReader["nome"] != DBNull.Value ? dReader["nome"].ToString() : string.Empty;
                            listAtividades.Add(atividade);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listAtividades;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeDD.GetAllAtividades()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static bool updateCicloAtualAtividade(int novo_ciclo, int id_atividade)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "UPDATE Atividade " +
                    "SET ciclo_atual = @ciclo_atual " +
                    "WHERE id_atividade = @id_atividade";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@ciclo_atual", novo_ciclo, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", id_atividade, tipoDadoBD.Integer);
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
                throw new Exception("[AtividadeDD.updateCicloAtualAtividade()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static int getCicloAtualAtividade(int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT ciclo_atual FROM Atividade " +
                    "WHERE id_atividade = @id_atividade";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", idAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    dReader.Read();
                    int cicloAtual = Conversao.FieldToInteger(dReader["ciclo_atual"]);
                    return cicloAtual;
                }
                else
                {
                    throw new Exception("[AtividadeDD.getCicloAtualAtividade()]: Não foi possível localizar o ciclo da Atividade.");
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeDD.getCicloAtualAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        public static Atividade getAtividadeByIdAtividade(int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT * FROM Atividade " +
                    "WHERE id_atividade = @id_atividade";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", idAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    dReader.Read();
                    Atividade ativ = new Atividade();
                    ativ.Id = Conversao.FieldToInteger(dReader["id_atividade"]);
                    ativ.Nome = Conversao.FieldToString(dReader["nome"]);
                    ativ.RepetirTarefa = Conversao.FieldToBoolean(dReader["repetir_tarefa"]);

                    conexao.Close();
                    dReader.Close();
                    return ativ;
                }
                else
                {
                    throw new Exception("[AtividadeDD.getAtividadeByIdAtividade()]: Não foi possível localizar a Atividade.");
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeDD.getAtividadeByIdAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        public static List<Atividade> getAllAtivExecutarByUsuario(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Atividade where id_usuario_executor = " + Convert.ToString(idUsuario);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                //IDbDataParameter parametro = command.CreateParameter();
                //DataBase.getParametroCampo(ref parametro, "@campo", campo.Trim(), tipoDadoBD.VarChar, clienteWeb.BancoCliente.Provider, campo.Trim().Length);
                //DataBase.getParametroCampo(ref parametro, "@campo", "gg", tipoDadoBD.VarChar, "ff".Length);
                //command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<Atividade> listAtividades = new List<Atividade>();
                        while (dReader.Read())
                        {
                            Atividade atividade = new Atividade();
                            atividade.Nome = Conversao.FieldToString(dReader["nome"]);
                            atividade.Id = Conversao.FieldToInteger(dReader["id_atividade"]);
                            atividade.Descricao = Conversao.FieldToString(dReader["descricao"]);
                            listAtividades.Add(atividade);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listAtividades;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeDD.getAllAtivExecutarByUsuario()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static List<Atividade> getAllAtivAdicionadasByUsuario(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Atividade where id_usuario_criador = " + Convert.ToString(idUsuario);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<Atividade> listAtividades = new List<Atividade>();
                        while (dReader.Read())
                        {
                            Atividade atividade = new Atividade();
                            atividade.Nome = Conversao.FieldToString(dReader["nome"]);
                            atividade.Id = Conversao.FieldToInteger(dReader["id_atividade"]);
                            atividade.Descricao = Conversao.FieldToString(dReader["descricao"]);
                            listAtividades.Add(atividade);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listAtividades;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeDD.getAllAtivAdicionadasByUsuario()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }
        
        public static bool addAtividade(Atividade ativ)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "INSERT INTO Atividade " +
                    "(id_status, id_usuario_executor, id_usuario_criador, nome, repetir_tarefa, ciclo_atual) " +
                    "VALUES (@id_status, @id_usuario_executor, @id_usuario_criador, @nome, @repetir_tarefa, @ciclo_atual)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                ExecuteAddAndUpdate(ativ, command);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                command.ExecuteNonQuery();

                if (transacao != null) transacao.Commit();


                return true;
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeDD.addAtividade()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static bool updateAtividade(Atividade atividade)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "UPDATE Atividade " +
                    "SET id_status = @id_status, " +
                    "nome = @nome, " +
                    "data_finalizacao = @data_finalizacao, " +
                    "data_criacao = @data_criacao, " +
                    "repetir_tarefa = @repetir_tarefa, " +
                    "descricao = @descricao " +
                    "WHERE id_atividade = @id_atividade";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = ExecuteAddAndUpdate(atividade, command);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", atividade.Id, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@data_finalizacao", atividade.DataFinalizacao, tipoDadoBD.DateTime);
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
                throw new Exception("[AtividadeDD.updateAtividade()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        private static IDbDataParameter ExecuteAddAndUpdate(Atividade atividade, IDbCommand command)
        {
            IDbDataParameter parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_status", atividade.IdStatus, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_usuario_executor", atividade.IdUsuarioExecutor, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@id_usuario_criador", atividade.IdUsuarioCriador, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@nome", atividade.Nome, tipoDadoBD.VarChar);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@repetir_tarefa", atividade.RepetirTarefa, tipoDadoBD.Boolean);
            command.Parameters.Add(parametro);

            parametro = command.CreateParameter();
            DataBase.getParametroCampo(ref parametro, "@ciclo_atual", atividade.CicloAtual, tipoDadoBD.Integer);
            command.Parameters.Add(parametro);

            return parametro;
        }
    }
}
