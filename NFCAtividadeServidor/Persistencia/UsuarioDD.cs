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
                        usuario.Id = Int32.Parse(dReader["id_usuario"].ToString());
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
                            usuario.Id = Conversao.FieldToInteger(dReader["id_usuario"]);
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
    }
}
