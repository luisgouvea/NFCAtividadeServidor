using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDB
{
    public class Conversao
    {
        public static decimal StringToDecimal(string valor)
        {
            try
            {
                if ((valor != null) && (valor.Trim() != ""))
                {
                    return Convert.ToDecimal(valor.Replace('.', ','));
                }
            }
            catch (Exception exp)
            {
            }

            return 0.00m;
        }

        public static DateTime StringToDateTime(string valor)
        {
            try
            {
                if (valor != null)
                {
                    if (valor.Trim() != "")
                    {
                        try
                        {
                            return Convert.ToDateTime(valor);
                        }
                        catch (Exception exp)
                        {
                            return DateTime.MinValue;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
            }
            return DateTime.MinValue;
        }

        public static string DateTimeToString(DateTime valor)
        {
            try
            {
                if (valor != null)
                {
                    if (valor != DateTime.MinValue)
                        return valor.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception exp)
            {
            }
            return "";
        }

        public static string DateTimeToStringNFe(DateTime valor)
        {
            try
            {
                if (valor != null)
                {
                    if (valor != DateTime.MinValue)
                        return valor.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception exp)
            {
            }
            return "";
        }

        public static string DateTimeToStringNFeUTC(DateTime valor)
        {
            if (valor != null)
            {
                if (valor != DateTime.MinValue)
                    return valor.ToString("yyyy-MM-ddTHH:mm:sszzz");
            }
            return "";
        }

        public static DateTime StringNFeUTCToDateTime(string valor)
        {
            DateTime dataDefault = DateTime.MinValue;

            if (valor != null)
            {
                bool parsed = DateTime.TryParse(valor, out dataDefault);

                if (parsed)
                {
                    return dataDefault;
                }
                else
                {
                    DateTimeOffset offset = DateTimeOffset.Parse(valor);
                    return offset.DateTime;
                }
            }
            else
            {
                return dataDefault;
            }
        }

        // ** Conversão Banco de Dados **
        public static string FieldToString(object valor)
        {
            string auxString = "";
            if (valor != DBNull.Value)
                auxString = valor.ToString();
            return auxString;
        }

        public static int FieldToInteger(object valor)
        {
            int auxInt = new int();
            if (valor != DBNull.Value)
                auxInt = Convert.ToInt32(valor);
            return auxInt;
        }

        public static uint FieldToUInteger(object valor)
        {
            uint auxInt = new uint();

            if (valor != DBNull.Value)
                auxInt = Convert.ToUInt32(valor);

            return auxInt;
        }

        public static long FieldToLongInteger(object valor)
        {
            long auxInt = new long();
            if (valor != DBNull.Value)
                auxInt = Convert.ToInt64(valor);
            return auxInt;
        }

        public static DateTime FieldToDateTime(object valor)
        {
            DateTime auxDateTime = new DateTime();
            if (valor != DBNull.Value)
                auxDateTime = Convert.ToDateTime(valor);
            return auxDateTime;
        }

        public static bool FieldToBoolean(object valor)
        {
            bool auxBool = new bool();
            if (valor != DBNull.Value)
                auxBool = Convert.ToBoolean(valor);
            return auxBool;
        }

        public static float FieldToFloat(object valor)
        {
            float auxFloat = new float();
            if (valor != DBNull.Value)
                auxFloat = float.Parse(valor.ToString());
            return auxFloat;
        }

        public static double FieldToDouble(object valor)
        {
            double auxDouble = new double();
            if (valor != DBNull.Value)
                auxDouble = double.Parse(valor.ToString());
            return auxDouble;
        }

        public static decimal FieldToDecimal(object valor)
        {
            decimal auxDouble = new decimal();
            if (valor != DBNull.Value)
                auxDouble = decimal.Parse(valor.ToString());
            return auxDouble;
        }

        public static byte[] FieldToByteArray(object valor)
        {
            byte[] auxByte = null;
            if (valor != DBNull.Value)
                auxByte = (byte[])valor;
            return auxByte;
        }

        /// <summary>
        /// Função para ser usada ao setar uma coluna DateTime do banco de dados
        /// </summary>
        /// <param name="valor">Deve-se ser informado um valor do tipo Datetime ou String</param>
        /// <returns></returns>
        public static object DateTimeToField(object valor)
        {
            DateTime data = DateTime.MinValue;
            object auxField = DBNull.Value;

            if (valor != null)
            {
                if (valor.GetType() == typeof(DateTime)) data = (DateTime)valor;
                else if (valor.GetType() == typeof(string)) data = StringToDateTime((string)valor);
                else if (valor.GetType() == typeof(String)) data = StringToDateTime((String)valor);
            }

            if ((data != null) && (data != DateTime.MinValue)) auxField = data;

            return auxField;
        }

        public static object StringToField(object valor)
        {
            object auxField = DBNull.Value;
            if (valor != null)
                if (valor.ToString().Trim() != "")
                    auxField = valor;
            return auxField;
        }

        public static object IntegerToField(object valor)
        {
            object auxField = DBNull.Value;
            if (valor != null)
                if (valor.ToString().Trim() != "")
                    auxField = valor;
            return auxField;
        }

        public static object ByteArrayToField(object valor)
        {
            object auxField = DBNull.Value;
            if (valor != null)
                auxField = valor;
            return auxField;
        }

        public static string apenasDigitos(string texto)
        {
            string resultado = "";
            foreach (char c in texto)
            {

                if (char.IsDigit(c))
                    resultado += c;
            }
            return resultado;
        }
    }
}
