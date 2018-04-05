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

        public static Boolean addTag(TAG tag)
        {
            return Persistencia.TagDD.addTag(tag);
        }

        public static List<TAG> getAllTagsByIdAtividade(int idAtividade)
        {
            return Persistencia.TagDD.getTagsByAtividade(idAtividade);
        }

        public static List<TAG> newGetAllTagsByIdAtividade(int idAtividade)
        {
            List<TAG> listTags = Persistencia.TagDD.getTagsByAtividade(idAtividade);
            foreach (TAG tag in listTags)
            {
                int id = tag.Id;
                List<TAG> listAntecessoras = Persistencia.TagDD.getTagsAntecessorasModel(id);
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
