using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Modelos;
using System.Data;
using LibraryDB;

namespace Persistencia
{
    public class AtividadeFluxoCorretoDD
    {
        public static AtividadeFluxoCorreto getUltimoCheckCorretoByIdAtividade(int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT TOP 1 * FROM AtividadeFluxoCorreto " +
                    "WHERE id_atividade = @id_atividade ORDER BY id_atividade_fluxo_correto desc";

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
                    AtividadeFluxoCorreto atividadeFluxo = new AtividadeFluxoCorreto();
                    atividadeFluxo.Id = Conversao.FieldToInteger(dReader["id_atividade_fluxo_correto"]);
                    atividadeFluxo.IdAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
                    atividadeFluxo.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
                    atividadeFluxo.Ciclo = Conversao.FieldToInteger(dReader["ciclo"]);
                    return atividadeFluxo;
                }
                else
                {
                    //throw new Exception("[AtividadeFluxoCorretoDD.getUltimoCheckCorretoAtividade()]: Não foi possível localizar a TAG.");
                    return null;
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeFluxoCorretoDD.getUltimoCheckCorretoAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        public static bool addFluxoCorreto(AtividadeFluxoCorreto fluxoCorreto)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {
                string sql = "INSERT INTO AtividadeFluxoCorreto " +
                    "(id_atividade, id_tarefa, ciclo, data_check) " +
                    "VALUES (@id_atividade, @id_tarefa, @ciclo, @data_check)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", fluxoCorreto.IdAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tarefa", fluxoCorreto.IdTarefa, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@ciclo", fluxoCorreto.Ciclo, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@data_check", fluxoCorreto.dataCheck, tipoDadoBD.DateTime);
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
                throw new Exception("[AtividadeFluxoCorretoDD.addFluxoCorreto()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByCicloAndIdAtividade(int ciclo, int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT * FROM AtividadeFluxoCorreto " +
                    "WHERE id_atividade = @id_atividade " +
                    "AND " +
                    "ciclo = @ciclo";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", idAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@ciclo", ciclo, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<AtividadeFluxoCorreto> listaFluxos = new List<AtividadeFluxoCorreto>();
                        while (dReader.Read())
                        {
                            AtividadeFluxoCorreto ativFluxoCorreto = new AtividadeFluxoCorreto();
                            ativFluxoCorreto.Id = Conversao.FieldToInteger(dReader["id_atividade_fluxo_correto"]);
                            ativFluxoCorreto.IdAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
                            ativFluxoCorreto.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            ativFluxoCorreto.Ciclo = Conversao.FieldToInteger(dReader["ciclo"]);
                            listaFluxos.Add(ativFluxoCorreto);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listaFluxos;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeFluxoCorretoDD.getAllCheckByCicloAndIdAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByIdAtividade(int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from AtividadeFluxoCorreto where id_atividade = " + Convert.ToString(idAtividade);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<AtividadeFluxoCorreto> listaFluxos = new List<AtividadeFluxoCorreto>();
                        while (dReader.Read())
                        {
                            AtividadeFluxoCorreto ativFluxoCorreto = new AtividadeFluxoCorreto();
                            ativFluxoCorreto.Id = Conversao.FieldToInteger(dReader["id_atividade_fluxo_correto"]);
                            ativFluxoCorreto.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            ativFluxoCorreto.Ciclo = Conversao.FieldToInteger(dReader["ciclo"]);
                            listaFluxos.Add(ativFluxoCorreto);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listaFluxos;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeFluxoCorretoDD.getAllCheckByIdAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByMonthCheckAndIdAtividade(DateTime dataCheck, int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from AtividadeFluxoCorreto " +
                    "where MONTH(data_check) = MONTH(@data_check) " +
                    "AND " +
                    "id_atividade = @id_atividade " +
                    "ORDER BY id_atividade_fluxo_correto desc";


                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@data_check", dataCheck, tipoDadoBD.DateTime);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", idAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<AtividadeFluxoCorreto> listaFluxos = new List<AtividadeFluxoCorreto>();
                        while (dReader.Read())
                        {
                            AtividadeFluxoCorreto ativFluxoCorreto = new AtividadeFluxoCorreto();
                            ativFluxoCorreto.Id = Conversao.FieldToInteger(dReader["id_atividade_fluxo_correto"]);
                            ativFluxoCorreto.dataCheck = Conversao.FieldToDateTime(dReader["data_check"]);
                            ativFluxoCorreto.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            ativFluxoCorreto.Ciclo = Conversao.FieldToInteger(dReader["ciclo"]);
                            listaFluxos.Add(ativFluxoCorreto);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listaFluxos;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeFluxoCorretoDD.getAllCheckByDataCheck()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByCicloAndDayCheckAndIdAtividade(DateTime dataCheck, int ciclo, int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from AtividadeFluxoCorreto " +
                    "where DAY(data_check) = DAY(@data_check) " +
                    "AND " +
                    "id_atividade = @id_atividade " +
                    "AND " +
                    "ciclo = @ciclo " +
                    "ORDER BY id_atividade_fluxo_correto desc";


                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@data_check", dataCheck, tipoDadoBD.DateTime);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", idAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@ciclo", ciclo, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<AtividadeFluxoCorreto> listaFluxos = new List<AtividadeFluxoCorreto>();
                        while (dReader.Read())
                        {
                            AtividadeFluxoCorreto ativFluxoCorreto = new AtividadeFluxoCorreto();
                            ativFluxoCorreto.Id = Conversao.FieldToInteger(dReader["id_atividade_fluxo_correto"]);
                            ativFluxoCorreto.dataCheck = Conversao.FieldToDateTime(dReader["data_check"]);
                            ativFluxoCorreto.IdTarefa = Conversao.FieldToInteger(dReader["id_tarefa"]);
                            ativFluxoCorreto.Ciclo = Conversao.FieldToInteger(dReader["ciclo"]);
                            listaFluxos.Add(ativFluxoCorreto);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listaFluxos;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeFluxoCorretoDD.getAllCheckByCicloAndDayCheckAndIdAtividade()]: " + exp.Message);
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
