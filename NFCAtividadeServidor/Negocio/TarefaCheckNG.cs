using System;
using Persistencia.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarefaCheckNG
    {
        public static bool addRegistroCheckNFC(Tarefa tarefa)
        {
            TarefaCheck tarefaCheck = new TarefaCheck();
            tarefaCheck.IdTarefa = tarefa.Id;
            tarefaCheck.DataExecucao = DateTime.Now;
            tarefaCheck.NomeTarefa = tarefa.Nome;
            return Persistencia.TarefaCheckDD.addRegistroCheckNFC(tarefaCheck);
        }

        public static List<TarefaCheck> getAllRegistroCheckNFCByIdsTarefa(int idAtividade)
        {
            List<Tarefa> listaTarefasDaAtividade = TarefaNG.getAllTarefasByIdAtividade(idAtividade);
            List<string> listaIds = getIdsTarefaRegistroCheckNFC(listaTarefasDaAtividade);
            return Persistencia.TarefaCheckDD.getHistoricoCheckNFCByIdsTarefa(listaIds);
        }

        public static bool realizarCheck(int idTagCheck, int idTarefa)
        {

            Tarefa tarefaSelecionadaUsuario = null;
            Tarefa tarefaDaTag = null;
            bool checkCorreto = true;
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
                addRegistroCheckNFC(tarefaSelecionadaUsuario);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao inserir o registro de Check: " + e.Message);
            }

            try
            {
                //get tarefa que a partir da TAG que o usuario realizou o check de nfc
                tarefaDaTag = TarefaNG.getTarefaByTagAndTarefa(idTagCheck, idTarefa);
            }
            catch
            {
                return false;
                //TODO: TRATAR
                //throw new Exception("A TAG que você realizou o check, não está com vinculada com a tarefa escolhida");
            }

            try
            {
                //verificar se a tarefa que o usuario checkou eh a tarefa correta de acordo com as suas regras

                Atividade ativ = AtividadeNG.getAtividadeByIdAtividade(tarefaDaTag.IdAtividade);

                if (ativ.RepetirTarefa)
                {
                    // TEM CICLO
                    int cicloAtual = AtividadeNG.getCicloAtualAtividade(ativ.Id);
                    List<AtividadeFluxoCorreto> listFluxos = AtividadeFluxoCorretoNG.getAllCheckByCicloAndIdAtividade(cicloAtual, ativ.Id);

                    if (listFluxos == null || listFluxos.Count == 0) // nao tem nenhum registro de CHECK
                    {
                        if (tarefaDaTag.IniciaFluxo == false)
                        {
                            return false;
                        }
                        //add na tabela de fluxo correto
                        AtividadeFluxoCorreto fluxoCorreto = new AtividadeFluxoCorreto();
                        fluxoCorreto.Ciclo = cicloAtual;
                        fluxoCorreto.IdTarefa = tarefaDaTag.Id;
                        fluxoCorreto.IdAtividade = ativ.Id;
                        AtividadeFluxoCorretoNG.addFluxoCorreto(fluxoCorreto);
                    }
                    else // ja foi realizado algum check ...
                    {
                        AtividadeFluxoCorreto ultimoCheck = AtividadeFluxoCorretoNG.getUltimoCheckCorretoByIdAtividade(ativ.Id);
                        
                        #region Logica de precedencia
                        List<TarefaPrecedente> listaAnte = TarefaPrecedenteNG.getTarefasAntecessorasCheck(tarefaDaTag.Id);
                        int tamanhoListaAnte = listaAnte.Count();
                        int count = 0;
                        foreach (AtividadeFluxoCorreto fluxo in listFluxos)
                        {
                            foreach (TarefaPrecedente tarefaPrece in listaAnte)
                            {
                                if (tarefaPrece.IdTarefaAntecessora == fluxo.IdTarefa)
                                {
                                    count++;
                                }
                            }
                        }

                        if(count != tamanhoListaAnte)
                        {
                            return false;
                        }

                        #endregion

                        #region Logica de sucessao

                        int ultiIdTarefaExecutado = ultimoCheck.IdTarefa;
                        List<TarefaSucedente> listaSucedentes = TarefaSucedenteNG.getTarefasSucessorasCheck(ultiIdTarefaExecutado);
                        foreach (TarefaSucedente proximaTarefa in listaSucedentes)
                        {
                            if (proximaTarefa.IdTarefaProxima != tarefaDaTag.Id)
                            {
                                // a tarefa do check eh uma sucessora da ultima tarefa checada
                                //break;
                                return false;
                            }
                        }

                        #endregion

                        //add na tabela de fluxo correto
                        AtividadeFluxoCorreto fluxoCorreto = new AtividadeFluxoCorreto();
                        fluxoCorreto.Ciclo = cicloAtual;
                        fluxoCorreto.IdTarefa = tarefaDaTag.Id;
                        fluxoCorreto.IdAtividade = ativ.Id;
                        AtividadeFluxoCorretoNG.addFluxoCorreto(fluxoCorreto);

                        if (tarefaDaTag.FinalizaFluxo)
                        {
                            int novoCiclo = cicloAtual + 1;
                            //update na atividade coluna cicloAtual com o novo valor
                            AtividadeNG.updateCicloAtualAtividade(novoCiclo, ativ.Id);
                        }
                        
                    }
                }
                else
                {
                    // TODO: NAO TEM CICLO                    
                }
                return true;
                //List<TarefaPrecedente> listaAntecessores = TarefaPrecedenteNG.getTarefasAntecessoras(tarefaDaTag.Id);
                //List<Tarefa> listaTarefasDaAtividade = TarefaNG.getAllTarefasByIdAtividade(tarefaDaTag.IdAtividade);
                //List<string> listaIds = getIdsTarefaRegistroCheckNFC(listaTarefasDaAtividade, tarefaDaTag.Id);
                //List<TarefaCheck> listaChecksDaAtividade = Persistencia.TarefaCheckDD.getHistoricoCheckNFCByIdsTarefa(listaIds);
                //if (listaChecksDaAtividade.Count() == 0)
                //{
                //    // nunca foi realizado check
                //    if (listaAntecessores.Count() > 0)
                //    {
                //        checkCorreto = false;
                //    }
                //}
                //else
                //{
                //    foreach (TarefaCheck tarefaCheck in listaChecksDaAtividade)
                //    {
                //        foreach (Tarefa tarefa in listaAntecessores)
                //        {
                //            if (tarefa.Id == tarefaCheck.IdTarefa)
                //            {
                //                checkCorreto = true;
                //            }
                //            else
                //            {
                //                checkCorreto = false;
                //            }
                //        }
                //    }
                //}
                //return checkCorreto;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao verificar o Check realizado: " + e.Message);
            }
        }

        #region Metodos privados da classe

        /// <summary>
        /// Metodo usado de auxilio para verificar se eh um check valido ou nao
        /// </summary>
        /// <param name="listaTarefa"></param>
        /// <param name="idTarefaTarget"></param>
        /// <returns></returns>
        public static List<string> getIdsTarefaRegistroCheckNFC(List<Tarefa> listaTarefa, int idTarefaTarget)
        {
            List<string> listaIds = new List<string>();
            foreach (Tarefa tarefa in listaTarefa)
            {
                if (tarefa.Id != idTarefaTarget)
                {
                    listaIds.Add(Convert.ToString(tarefa.Id));
                }
            }

            return listaIds;
        }

        /// <summary>
        /// Metodo usado de auxilio para PEGAR TODO O REGISTRO DE CHECK NFC
        /// </summary>
        /// <param name="listaTarefa"></param>
        /// <param name="idTarefaTarget"></param>
        /// <returns></returns>
        public static List<string> getIdsTarefaRegistroCheckNFC(List<Tarefa> listaTarefa)
        {
            List<string> listaIds = new List<string>();
            foreach (Tarefa tarefa in listaTarefa)
            {
                listaIds.Add(Convert.ToString(tarefa.Id));
            }

            return listaIds;
        }
        #endregion

    }
}
