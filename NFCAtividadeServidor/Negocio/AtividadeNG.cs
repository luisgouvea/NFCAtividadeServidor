using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AtividadeNG
    {
        public static List<Atividade> GetAllAtividades()
        {
            return Persistencia.AtividadeDD.GetAllAtividades();
        }

        public static List<Atividade> getAllAtivExecutar(int idUsuario)
        {
            return Persistencia.AtividadeDD.getAllAtivExecutarByUsuario(idUsuario);
        }

        public static List<Atividade> getAllAtivAdicionadas(int idUsuario)
        {
            return Persistencia.AtividadeDD.getAllAtivAdicionadasByUsuario(idUsuario);
        }

        public static bool adicionarAtividade(Atividade atividade)
        {
            try
            {
                return Persistencia.AtividadeDD.addAtividade(atividade);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unique_AtividadeNome"))
                {
                    throw new Exception("Você já possui uma Atividade com esse nome. Informe outro nome!");
                }
                else
                {
                    throw ex;
                }
            }
        }

        public static Atividade getAtividadeByIdAtividade(int idAtividade)
        {
            return Persistencia.AtividadeDD.getAtividadeByIdAtividade(idAtividade);
        }

        public static int getCicloAtualAtividade(int idAtividade)
        {
            return Persistencia.AtividadeDD.getCicloAtualAtividade(idAtividade);
        }

        public static bool updateCicloAtualAtividade(int novoCiclo, int id_atividade)
        {
            return Persistencia.AtividadeDD.updateCicloAtualAtividade(novoCiclo, id_atividade);
        }
    }
}
