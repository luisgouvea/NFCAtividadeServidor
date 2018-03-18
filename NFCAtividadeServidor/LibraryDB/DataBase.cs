using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace LibraryDB
{
    public class DataBase
    {
        public static IDbConnection getConection()
        {
            string bdConnectionString = null;
            return (IDbConnection)MSSQL.getSqlConnection(bdConnectionString);
        }

        public static IDbDataAdapter getDataAdapter(string sql, IDbConnection connection)
        {
            return (IDbDataAdapter)MSSQL.getSqlDataAdapter(sql, connection);
        }

        public static IDataReader getDataReader(string sql, IDbConnection connection)
        {
            return (IDataReader)MSSQL.getSqlDataReader(sql, connection);
        }

        public static IDbCommand getCommand(string sql, IDbConnection connection)
        {
            return getCommand(sql, connection, null);
        }

        public static IDbCommand getCommand(string sql, IDbConnection connection, IDbTransaction transaction)
        {
            return (IDbCommand)MSSQL.getSqlCommand(sql, connection, transaction);
        }

        public static bool executeSQL(string sql, IDbConnection connection)
        {
            try
            {
                IDbCommand cmd = getCommand(sql, connection);
                connection.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception exp)
            {
                connection.Close();
                throw new Exception("Não foi possivel executar o comando: " + exp.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void getParametroCampo(ref IDbDataParameter dbParametro, string nome, object valor, tipoDadoBD tpDado)
        {
            getParametroCampo(ref dbParametro, nome, valor, tpDado, 0);
        }

        public static void getParametroCampo(ref IDbDataParameter dbParametro, string nome, object valor, tipoDadoBD tpDado, int tamDado)
        {
            MSSQL.setSqlParametroCampo(ref dbParametro, nome, valor, tamDado, tpDado);
        }

        private static tipoProvedorBD getTipoProvedorBD()
        {
            return tipoProvedorBD.SQLServer;
        }
    }

    public enum tipoDadoBD
    {
        VarChar,
        Numeric,
        Integer,
        Boolean,
        Binary,
        DateTime,
        Xml
    }

    public enum tipoProvedorBD
    {
        [Description("System.Data.SqlClient")]
        SQLServer,
        [Description("FirebirdSql.Data.FirebirdClient")]
        Firebird,
        [Description("Oracle.DataAccess.Client")]
        Oracle,
        [Description("MySql.Data.MySqlClient")]
        MySQL,
        [Description("PostgreSql.Data.Client")]
        PostgreSql
    }

    //internal class POSTGRESQL
    //{
    //    internal static NpgsqlConnection getSqlConnection(string strConnect)
    //    {
    //        try
    //        {
    //            NpgsqlConnection conn = new NpgsqlConnection(strConnect);
    //            return conn;
    //        }
    //        catch (Exception exp)
    //        {
    //            return null;
    //        }
    //    }

    //    internal static NpgsqlDataAdapter getSqlDataAdapter(string sql, IDbConnection conn)
    //    {
    //        try
    //        {
    //            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, (NpgsqlConnection)conn);
    //            return da;
    //        }
    //        catch (Exception exp)
    //        {
    //            return null;
    //        }
    //    }

    //    internal static NpgsqlCommand getSqlCommand(string sql, IDbConnection conn, IDbTransaction trans)
    //    {
    //        try
    //        {
    //            NpgsqlCommand cmd = new NpgsqlCommand(sql, (NpgsqlConnection)conn, (NpgsqlTransaction)trans);
    //            return cmd;
    //        }
    //        catch (Exception exp)
    //        {
    //            return null;
    //        }
    //    }

    //    internal static NpgsqlDataReader getSqlDataReader(string sql, IDbConnection conn)
    //    {
    //        try
    //        {
    //            NpgsqlCommand cmd = new NpgsqlCommand(sql, (NpgsqlConnection)conn);
    //            conn.Open();
    //            NpgsqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    //            if (dr.HasRows)
    //                return dr;
    //            else
    //            {
    //                dr.Dispose();
    //                dr.Close();
    //                return null;
    //            }
    //        }
    //        catch (Exception exp)
    //        {
    //            return null;
    //        }
    //    }

    //    internal static void setSqlParametroCampo(ref IDbDataParameter dp, string nome, object valor, int tamDado, tipoDadoBD tpDado)
    //    {
    //        ((NpgsqlParameter)dp).ParameterName = nome;
    //        ((NpgsqlParameter)dp).DbType = getTypeBD(tpDado);
    //        ((NpgsqlParameter)dp).Value = valor;
    //        if (tamDado < 4000)
    //            ((NpgsqlParameter)dp).Size = 4000;
    //        else
    //            ((NpgsqlParameter)dp).Size = -1;
    //    }

    //    private static DbType getTypeBD(tipoDadoBD tpDado)
    //    {
    //        switch (tpDado)
    //        {
    //            case tipoDadoBD.VarChar:
    //                return DbType.String;
    //            case tipoDadoBD.Integer:
    //                return DbType.Int32;
    //            case tipoDadoBD.Numeric:
    //                return DbType.Double;
    //            case tipoDadoBD.Boolean:
    //                return DbType.Boolean;
    //            case tipoDadoBD.Binary:
    //                return DbType.Binary;
    //            case tipoDadoBD.DateTime:
    //                return DbType.DateTime;
    //            case tipoDadoBD.Xml:
    //                return DbType.Xml;
    //            default:
    //                return DbType.String;
    //        }
    //    }
    //}

    //** Cada classe abaixo representa o engine que o DataBaseSW vai suportar.
    internal class MSSQL
    {

        internal static SqlConnection getSqlConnection(string strConnect)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strConnect);
                return conn;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        internal static SqlDataAdapter getSqlDataAdapter(string sql, IDbConnection conn)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, (SqlConnection)conn);
                return da;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        internal static SqlCommand getSqlCommand(string sql, IDbConnection conn, IDbTransaction trans)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, (SqlConnection)conn, (SqlTransaction)trans);
                return cmd;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        internal static SqlDataReader getSqlDataReader(string sql, IDbConnection conn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, (SqlConnection)conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                    return dr;
                else
                {
                    dr.Dispose();
                    dr.Close();
                    return null;
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        internal static void setSqlParametroCampo(ref IDbDataParameter dp, string nome, object valor, int tamDado, tipoDadoBD tpDado)
        {
            ((SqlParameter)dp).ParameterName = nome;
            ((SqlParameter)dp).DbType = getTypeBD(tpDado);
            ((SqlParameter)dp).Value = valor;
            if (tamDado < 4000)
                ((SqlParameter)dp).Size = 4000;
            else
                ((SqlParameter)dp).Size = -1;
        }

        private static DbType getTypeBD(tipoDadoBD tpDado)
        {
            switch (tpDado)
            {
                case tipoDadoBD.VarChar:
                    return DbType.String;
                case tipoDadoBD.Integer:
                    return DbType.Int32;
                case tipoDadoBD.Numeric:
                    return DbType.Double;
                case tipoDadoBD.Boolean:
                    return DbType.Boolean;
                case tipoDadoBD.Binary:
                    return DbType.Binary;
                case tipoDadoBD.DateTime:
                    return DbType.DateTime;
                case tipoDadoBD.Xml:
                    return DbType.Xml;
                default:
                    return DbType.String;
            }
        }

    }
}
