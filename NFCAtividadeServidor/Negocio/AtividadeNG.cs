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

        public static bool realizarCheck(int idTagCheck, int idTarefa)
        {

            Tarefa tarefaSelecionadaUsuario = null;
            Tarefa tarefaDaTag = null;
            try
            {
                //get da tarefa da listagem do front
                tarefaSelecionadaUsuario = TarefaNG.getTarefa(idTarefa);
                if (tarefaSelecionadaUsuario == null)
                {
                    throw new Exception("Não foi possível encontrar a tarefa escolhida no banco de dados.");
                }

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível encontrar a tarefa escolhida: " + e.Message);
            }

            try
            {
                //realizar historico dessa tarefa
                TarefaCheckNG.addRegistroCheckNFC(tarefaSelecionadaUsuario);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao inserir o registro de Check: " + e.Message);
            }

            try
            {
                //get tarefa que a partir da TAG que o usuario realizou o check de nfc
                tarefaDaTag = TarefaNG.getTarefaByTag(idTagCheck);
                if (tarefaDaTag == null)
                {
                    throw new Exception("Não foi possível encontrar a tarefa vinculada a Tag que você realizou o Check.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao buscar a Tarefa vinculada a Tag que você realizou o Check: " + e.Message);
            }

            if(tarefaSelecionadaUsuario.Id != tarefaDaTag.Id)
            {
                throw new Exception("A TAG que você realizou o check, não está com vinculada com a tarefa escolhida!");
            }

            try
            {
                //verificar se a tarefa que o usuario checkou eh a tarefa correta de acordo com as suas regras
                List<Tarefa> listaAntecessores = TarefaNG.getTarefasAntecessoras(tarefaDaTag.Id);
                List<TarefaCheck> listaChecksDaAtividade = TarefaCheckNG.getHistoricoCheckNFCByIdAtividade(tarefaDaTag.IdAtividade, tarefaDaTag.Id);
                foreach(TarefaCheck tarefaCheck in listaChecksDaAtividade)
                {
                    foreach (Tarefa tarefa in listaAntecessores)
                    {
                        if(tarefa.Id == tarefaCheck.IdTarefa)
                        {
                            continue;
                        }
                        return false;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao verificar o Check realizado: " + e.Message);
            }
        }
    }
}
