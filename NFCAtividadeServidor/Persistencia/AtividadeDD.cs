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

        public static List<Atividade> getAllAtivExecutarByUsuario(string idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Atividade where id_usuario_executor = " + idUsuario;

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
                throw new Exception("[AtividadeDD.getAllAtivExecutarByUsuario()]: " + exp.Message);
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
