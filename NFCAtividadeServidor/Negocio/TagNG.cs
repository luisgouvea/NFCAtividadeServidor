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
            List<TAG> listaTags = Persistencia.TagDD.getTagsByAtividade(idAtividade);
            foreach (TAG tag in listaTags)
            {
                int id = tag.Id;
                string nome = tag.Nome;
                
                List<string> listAntecessoras = Persistencia.TagDD.getTagsAntecessoras(id);
                List<string> listFinal = new List<string>();
                foreach (string tagAnt in listAntecessoras)
                {
                    string format  = tagAnt + " -> "  + nome;
                    listFinal.Add(format);
                }

                tag.listAntecessores = listFinal;
            }
            return listaTags;
        }

        public static Boolean addTag(TAG tag, int idAtividade)
        {
            return Persistencia.TagDD.addTag(tag, idAtividade);
        }

        public static List<TAG> getAllTagsByIdAtividade(int idAtividade)
        {
            return Persistencia.TagDD.getTagsByAtividade(idAtividade);
        }
    }
}
