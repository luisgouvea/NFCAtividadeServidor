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
            return Persistencia.TagDD.addTag(tag);
        }

        public static List<TAG> getAllTagsByIdUsuario(int idUsuario)
        {
            List<TAG> listTags = Persistencia.TagDD.getTagsByUsuario(idUsuario);
            return listTags;
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
