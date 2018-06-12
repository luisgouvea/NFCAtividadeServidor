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
    public class UsuarioDD
    {
        public static Usuario getUsuario(string login, string senha)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                //string sql = "select * from Usuario where login = " + login + " AND " + "senha = " + senha;
                string sql = "select * from Usuario where nome = @nome and senha = @senha";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@nome", login.Trim(), tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@senha", senha, tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    dReader.Read();
                    Usuario usuario = new Usuario();
                    if (dReader["id_usuario"] != DBNull.Value)
                    {                        
                        usuario.IdUsuario = Int32.Parse(dReader["id_usuario"].ToString());
                    }
                    conexao.Close();
                    dReader.Close();
                    return usuario;
                }
                else
                {
                    throw new Exception("[UsuarioDD.getUsuario()]: Não foi possível localizar o usuário.");
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[UsuarioDD.getUsuario()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        public static int addUsuario(Usuario usuario)
        {
            IDbConnection conexao = null;
            IDbTransaction transacao = null;

            try
            {

                string sql = "INSERT INTO Usuario " +
                    "(nome, login, senha) " +
                    "VALUES (@nome, @login, @senha); SELECT SCOPE_IDENTITY();";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@nome", usuario.Nome, tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@login", usuario.Login, tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@senha", usuario.Senha, tipoDadoBD.VarChar);
                command.Parameters.Add(parametro);

                conexao.Open();
                transacao = conexao.BeginTransaction();
                command.Transaction = transacao;

                //command.ExecuteNonQuery();
                int lastId = Convert.ToInt32(command.ExecuteScalar());
                
                if (transacao != null) transacao.Commit();

                return lastId;
            }
            catch (Exception exp)
            {
                throw new Exception("[UsuarioDD.addUsuario()]: " + exp.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public static Usuario getUsuarioById(int idUsuario)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "select * from Usuario where id_usuario = @id_usuario";

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                IDbDataParameter parametro = command.CreateParameter();
                DataBase.getParametroCampo(ref parametro, "@id_usuario", idUsuario, tipoDadoBD.Integer);
                command.Parameters.Add(parametro);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    dReader.Read();
                    Usuario usuario = getDadosUsuario(dReader);
                    return usuario;
                }
                else
                {
                    throw new Exception("[UsuarioDD.getUsuarioById()]: Não foi possível localizar o usuário pelo identificado.");
                }
            }
            catch (Exception exp)
            {
                throw new Exception("[UsuarioDD.getUsuarioById()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        public static List<Usuario> listAllUsuarioAddAtivVincExecutor(int idUsuarioTarget)
        {
            IDbConnection conexao = null;
            IDataReader dReader = null;

            try
            {

                string sql = "SELECT * FROM Usuario where id_usuario != " + Convert.ToString(idUsuarioTarget);

                conexao = DataBase.getConection();
                IDbCommand command = DataBase.getCommand(sql, conexao);

                conexao.Open();
                dReader = command.ExecuteReader();

                if (dReader != null)
                {
                    try
                    {
                        List<Usuario> listUsuario = new List<Usuario>();
                        while (dReader.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.IdUsuario = Conversao.FieldToInteger(dReader["id_usuario"]);
                            usuario.Nome = Conversao.FieldToString(dReader["nome"]);
                            listUsuario.Add(usuario);
                        }

                        conexao.Close();
                        dReader.Close();
                        return listUsuario;
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Ocorreu um erro: " + exp.Message);
                    }
                }
                return null;
            }
            catch (Exception exp)
            {
                throw new Exception("[UsuarioDD.listAllUsuarioAddAtivVincExecutor()]: " + exp.Message);
            }
            finally
            {
                if (dReader != null) dReader.Close();
                if (conexao != null) conexao.Close();
            }
        }

        private static Usuario getDadosUsuario(IDataReader dReader)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = Conversao.FieldToString(dReader["nome"]);
            usuario.IdUsuario = Conversao.FieldToInteger(dReader["id_usuario"]);
            return usuario;
        }
    }
}
