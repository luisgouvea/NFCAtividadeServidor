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
                throw new Exception("[TagDD.getTagsByAtividade()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static Boolean addTag(TAG tag)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO Tag " +
                    "(id_atividade, comentario) " +
                    "VALUES (@id_atividade, @comentario)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_atividade", tag.IdAtividade, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@comentario", tag.Nome, tipoDadoBD.VarChar);
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
                throw new Exception("[TagDD.addTag()]: " + exp.Message);
            }
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

        public static List<TAG> getTagsAntecessorasModel(int idTag)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tag where id_tag in " +
                    "(" +
                        "select te.id_tag_antecessora from Tag t " +
                        "inner join TagEncadeamento te " +
                        "on t.id_tag = te.id_tag_target " +
                        "where te.id_tag_target = " + idTag
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
                        List<TAG> listAntecessoras = new List<TAG>();
                        while (dReader.Read())
                        {
                            int idPk = Conversao.FieldToInteger(dReader["id_tag"]);
                            string nome = Conversao.FieldToString(dReader["comentario"]);
                            int idAtividade = Conversao.FieldToInteger(dReader["id_atividade"]);
                            TAG tag = new TAG();
                            tag.Id = idPk;
                            tag.Nome = nome;
                            tag.IdAtividade = idAtividade;
                            listAntecessoras.Add(tag);
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
                throw new Exception("[TagDD.getTagsAntecessorasModel()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }

            return null;
        }

        public static Boolean deleteEncadeamentoTag(int id_tag_target)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "DELETE FROM TagEncadeamento " +
                    "WHERE id_tag_target = @id_tag_target";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tag_target", id_tag_target, tipoDadoBD.Integer);
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
                throw new Exception("[TagDD.insertEncadeamentoTag()]: " + exp.Message);
            }
        }

        public static Boolean insertEncadeamentoTag(int id_tag_target, int id_tag_antecessora)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO TagEncadeamento " +
                    "(id_tag_target, id_tag_antecessora) " +
                    "VALUES (@id_tag_target, @id_tag_antecessora)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tag_target", id_tag_target, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tag_antecessora", id_tag_antecessora, tipoDadoBD.Integer);
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
                throw new Exception("[TagDD.insertEncadeamentoTag()]: " + exp.Message);
            }
        }        
    }
}
