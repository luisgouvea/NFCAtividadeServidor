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
        public static Boolean addTag(TAG tag)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                //string sql = "INSERT INTO Tag " +
                //    "(id_usuario, nome, id_tag, palavra_chave) " +
                //    "VALUES (@id_usuario, @nome, @id_tag, @palavra_chave)";

                string sql = "INSERT INTO Tag " +
                    "(id_usuario, nome, id_tag) " +
                    "VALUES (@id_usuario, @nome, @id_tag)";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_usuario", tag.IdUsuario, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_tag", tag.Id, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@nome", tag.Nome, tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                //parametro = command.CreateParameter();
                //DataBase.getParametroCampo(ref parametro, "@palavra_chave", tag.PalavraChave, tipoDadoBD.VarChar);
                //command.Parameters.Add(parametro);

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

        public static List<TAG> getTagsByUsuario(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Tag where id_usuario = " + Convert.ToString(idUsuario);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

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
                            tag.Nome = Conversao.FieldToString(dReader["nome"]);
                            tag.IdUsuario = Conversao.FieldToInteger(dReader["id_usuario"]);
                            tag.PalavraChave = Conversao.FieldToString(dReader["palavra_chave"]);
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
                throw new Exception("[TagDD.getTagsByUsuario()]: " + exp.Message);
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
