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
            return Persistencia.AtividadeDD.addAtividade(atividade);
        }

        public static bool realizarCheck(int idTagCheck)
        {

            Tarefa tarefa = null;
            try
            {
                //get tarefa
                tarefa = TarefaNG.getTarefaByTag(idTagCheck);
                if (tarefa == null)
                {
                    throw new Exception("Não foi possível encontrar a tarefa vinculada a Tag que você realizou o Check.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao buscar a Tarefa vinculada a Tag que você realizou o Check: " + e.Message);
            }

            try
            {
                //realizar historico dessa tarefa
                //TarefaCheckNG.addRegistroCheckNFC(tarefa);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao inserir o registro de Check: " + e.Message);
            }

            try
            {
                //verificar se a tarefa que o usuario checkou eh a tarefa correta de acordo com as suas regras
                return true;
                //return AtividadeNG.adicionarAtividade(ativ);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao verificar o Check realizado: " + e.Message);
            }
        }
    }
}
