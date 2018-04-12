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
            return Persistencia.TagDD.addTag(tag);
        }

        public static List<TAG> getAllTagsByIdUsuario(int idUsuario)
        {
            List<TAG> listTags = Persistencia.TagDD.getTagsByUsuario(idUsuario);
            return listTags;
        }
    }
}
