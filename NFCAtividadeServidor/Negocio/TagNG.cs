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

        public static List<TAG> getAllTagsByIdAtividade(int idAtividade)
        {
            List<TAG> listTags = Persistencia.TagDD.getTagsByAtividade(idAtividade);
            foreach (TAG tag in listTags)
            {
                int id = tag.Id;
                List<TAG> listAntecessoras = Persistencia.TagDD.getTagsAntecessoras(id);
                tag.listaEncadeamento = listAntecessoras;
            }
            return listTags;
        }

        /**
         *  Sempre assumir que a lista de encadeamento do param tag (TAG), esta com todos os seus antecessores
        **/
        public static Boolean setarEncadeamentoTag(TAG tag)
        {
            int idTagTarget = tag.Id;
            Persistencia.TagDD.deleteEncadeamentoTag(idTagTarget); // delete all encadeamento
            foreach (TAG tagAnte in tag.listaEncadeamento)
            {
                Persistencia.TagDD.insertEncadeamentoTag(idTagTarget, tagAnte.Id); // cria novamente
            }
            return true;
        }        
    }
}
