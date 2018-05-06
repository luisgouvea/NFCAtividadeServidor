using System;
using Persistencia.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TagNG
    {
        public static Boolean addTag(TAG tag)
        {
            //tag.Id = Convert.ToInt32(generatedIdTag());
            try
            {
                Persistencia.TagDD.addTag(tag);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unique_TagNome"))
                {
                    throw new Exception("Você já possui uma TAG com esse nome. Informe outro nome!");
                }
                else
                {
                    throw ex;
                }
            }
            return true;
        }

        public static List<TAG> getAllTagsByIdUsuario(int idUsuario)
        {
            List<TAG> listTags = Persistencia.TagDD.getTagsByUsuario(idUsuario);
            return listTags;
        }

        public static TAG getTag(string identificadorTag)
        {
            return Persistencia.TagDD.getTagByIdTag(identificadorTag);
        }

        #region Métodos Privados
        private static long generatedIdTag()
        {
            DateTime foo = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            return unixTime;
        }
        #endregion
    }
}
