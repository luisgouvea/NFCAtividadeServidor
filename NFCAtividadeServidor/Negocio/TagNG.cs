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
        public static List<TAG> getTagsByAtividade(int idAtividade)
        {
            return Persistencia.TagDD.getTagsByAtividade(idAtividade);
        }
    }
}
