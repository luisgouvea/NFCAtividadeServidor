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
    public class TagDD
    {
        public static List<TAG> getTagsByAtividade(int idAtividade)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tag where id_atividade = " + Convert.ToString(idAtividade);

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
                        List<TAG> listTags = new List<TAG>();
                        while (dReader.Read())
                        {
                            TAG tag = new TAG();
                            tag.Nome = Conversao.FieldToString(dReader["comentario"]);
                            //tag.Nome = Conversao.FieldToString(dReader["nome"]);
                            tag.Id = Conversao.FieldToInteger(dReader["id_tag"]);
                            listTags.Add(tag);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listTags;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[AtividadeDD.getTagsByAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static List<string> getTagsAntecessoras(int idTag)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                //string sql = "select * from TagEncadeamento where id_tag_target = " + Convert.ToString(idTag);

                string sql = "select comentario from Tag where id_tag in " +
                    "(" +
                        "select te.id_tag_antecessora from Tag t " +
                        "inner join TagEncadeamento te " +
                        "on t.id_tag = te.id_tag_target " +
                        "where te.id_tag_target = " + idTag 
                        +
                    ")";

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
                        List<string> listAntecessoras = new List<string>();
                        while (dReader.Read())
                        {
                            string nome = dReader["comentario"] != DBNull.Value ? dReader["comentario"].ToString() : string.Empty;
                            listAntecessoras.Add(nome);
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
                throw new Exception("[TagDD.getTagsAntecessoras()]: " + exp.Message);
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
