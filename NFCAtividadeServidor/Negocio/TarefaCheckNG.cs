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
        public static bool addRegistroCheckNFC(Tarefa tarefa, int idStatusCheckNFC)
        {
            TarefaCheck tarefaCheck = new TarefaCheck();
            tarefaCheck.IdTarefa = tarefa.IdTarefa; // ID da tarefa
            tarefaCheck.IdStatusCheckNFC = idStatusCheckNFC;
            tarefaCheck.DataExecucao = DateTime.Now;
            tarefaCheck.Nome = tarefa.Nome;

            Atividade a = AtividadeNG.getAtividadeByIdAtividade(tarefa.IdAtividade);
            int cicloAtual = a.CicloAtual;
            if (cicloAtual == 0)
            {
                tarefaCheck.Ciclo = 1;
            }
            else
            {
                tarefaCheck.Ciclo = cicloAtual;
            }
            return Persistencia.TarefaCheckDD.addRegistroCheckNFC(tarefaCheck);
        }

        public static List<TarefaCheck> getAllRegistroCheckNFCByIdsTarefa(int idAtividade)
        {
            List<Tarefa> listaTarefasDaAtividade = TarefaNG.getAllTarefasByIdAtividade(idAtividade);
            List<string> listaIds = getIdsTarefaRegistroCheckNFC(listaTarefasDaAtividade);
            if (listaIds.Count == 0)
            {
                return null;
            }
            return Persistencia.TarefaCheckDD.getHistoricoCheckNFCByIdsTarefa(listaIds);
        }

        public static string[] realizarCheck(int identificadorTag, int idTarefa)
        {
            string[] result = new string[3];
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
                //get tarefa que a partir da TAG que o usuario realizou o check de nfc
                tarefaDaTag = TarefaNG.getTarefaByTagAndTarefa(identificadorTag, idTarefa);
            }
            catch
            {
                realizaHistoricoTarefa(tarefaSelecionadaUsuario, 1);
                result[0] = "invalido";
                result[1] = "A TAG que você realizou o check, não está vinculada com a tarefa escolhida."; // causa do erro
                result[2] = "Realize o check da tarefa de acordo com a TAG vinculada no momento de criação da Tarefa."; // solucao do erro
                return result;
                //return false;
                //TODO: TRATAR
                //throw new Exception("A TAG que você realizou o check, não está com vinculada com a tarefa escolhida");
            }

            try
            {
                //verificar se a tarefa que o usuario checkou eh a tarefa correta de acordo com as suas regras

                Atividade ativ = AtividadeNG.getAtividadeByIdAtividade(tarefaDaTag.IdAtividade);

                //if (ativ.RepetirTarefa)
                if (ativ.IdModoExecucao == 2) // Execucao por dia
                {
                    // TEM CICLO INFINITO
                    int cicloAtual = AtividadeNG.getCicloAtualAtividade(ativ.Id);
                    //listFluxos = AtividadeFluxoCorretoNG.getAllCheckByCicloAndIdAtividade(cicloAtual, ativ.Id);

                    List<AtividadeFluxoCorreto> listFluxos = null;

                    if (!string.IsNullOrWhiteSpace(ativ.DiaExecucao) && !string.IsNullOrEmpty(ativ.DiaExecucao))
                    {
                        // GET CHECKS POR DIA ESPECIFICO

                        DateTime hoje = DateTime.Now;
                        DateTime diaDoMes = hoje.AddDays(Convert.ToDouble(ativ.DiaExecucao));
                        if (diaDoMes.Month != DateTime.Now.Month)
                        {
                            result[0] = "invalido";
                            result[1] = "A atividade, não deve ser executada hoje."; // causa do erro
                            result[2] = "Realize o check das tarefas da Atividade " + ativ.Nome + " no dia " + ativ.DiaExecucao + " desse mês"; // solucao do erro
                            return result;
                        }
                        listFluxos = AtividadeFluxoCorretoNG.getAllCheckByMonthCheckAndIdAtividade(diaDoMes, ativ.Id);

                        if (ativ.NumMaximoCiclo != 0)
                        {
                            if (listFluxos != null && listFluxos.Count >= ativ.NumMaximoCiclo && listFluxos[0].dataCheck.Month == DateTime.Now.Month)
                            {
                                //passou do limite de ciclo
                                result[0] = "invalido";
                                result[1] = "Foi atingido o número total de ciclos completos da atividade. A atividade não pode ser mais executada NESSE MÊS"; // causa do erro
                                result[2] = "Realize o check das tarefas da Atividade, no próximo mês válido."; // solucao do erro
                                return result;
                            }
                        }
                    }
                    else
                    {
                        //GET FLUXOS POR DIA DE HOJE (NAO TEM LIMITACAO DE DIA)
                        listFluxos = AtividadeFluxoCorretoNG.getAllCheckByCicloAndDayCheckAndIdAtividade(DateTime.Now, cicloAtual, ativ.Id);

                        if (ativ.NumMaximoCiclo != 0)
                        {
                            if (listFluxos != null && listFluxos.Count >= ativ.NumMaximoCiclo && listFluxos[0].dataCheck.Day == DateTime.Now.Day)
                            {
                                //passou do limite de ciclo
                                result[0] = "invalido";
                                result[1] = "Foi atingido o número total de ciclos completos da atividade. A atividade não pode ser mais executada HOJE"; // causa do erro
                                result[2] = "Realize o check das tarefas da Atividade, no próximo dia válido."; // solucao do erro
                                return result;
                            }
                        }

                    }


                    if (listFluxos == null || listFluxos.Count == 0) // nao tem nenhum registro de CHECK
                    {
                        if (tarefaDaTag.IniciaFluxo == false)
                        {
                            realizaHistoricoTarefa(tarefaSelecionadaUsuario, 1); // 1 = invalido
                            result[0] = "invalido";
                            result[1] = "A tarefa escolhida, não inicia a sua atividade."; // causa do erro
                            result[2] = "Realize o check de uma Tarefa que Inicie o fluxo da sua atividade."; // solucao do erro
                            return result;
                            //return false;
                        }
                        //add na tabela de fluxo correto
                        AtividadeFluxoCorreto fluxoCorreto = new AtividadeFluxoCorreto();
                        fluxoCorreto.Ciclo = cicloAtual;
                        fluxoCorreto.IdTarefa = tarefaDaTag.IdTarefa;
                        fluxoCorreto.IdAtividade = ativ.Id;
                        fluxoCorreto.dataCheck = DateTime.Now;
                        AtividadeFluxoCorretoNG.addFluxoCorreto(fluxoCorreto);
                        TarefaNG.updateStatusExecucao(2, tarefaDaTag.IdTarefa);
                        AtividadeNG.updateStatusAtividade(ativ.Id, 2); // status = Em execucao
                        realizaHistoricoTarefa(tarefaSelecionadaUsuario, 2);

                        result[0] = "valido";
                        result[1] = "Ok! Pode executar a proxima tarefa!"; // proximoPasso
                        result[2] = "";
                        return result;
                    }
                    else // ja foi realizado algum check ...
                    {
                        AtividadeFluxoCorreto ultimoCheck = AtividadeFluxoCorretoNG.getUltimoCheckCorretoByIdAtividade(ativ.Id);

                        #region Logica de precedencia
                        List<TarefaPrecedente> listaAnte = TarefaPrecedenteNG.getTarefasAntecessorasCheck(tarefaDaTag.IdTarefa);
                        int tamanhoListaAnte = listaAnte.Count();
                        int count = 0;
                        List<int> listaFaltantes = new List<int>();
                        foreach (AtividadeFluxoCorreto fluxo in listFluxos)
                        {
                            foreach (TarefaPrecedente tarefaPrece in listaAnte)
                            {
                                if (tarefaPrece.IdTarefaAntecessora == fluxo.IdTarefa)
                                {
                                    count++;
                                }
                                else
                                {
                                    listaFaltantes.Add(tarefaPrece.IdTarefaAntecessora);
                                }
                            }
                        }

                        if (count != tamanhoListaAnte)
                        {
                            bool invalido = false;
                            foreach (int id in listaFaltantes)
                            {
                                if (naoChegaNoTarget(tarefaDaTag.IdTarefa, id) == false)
                                {
                                    invalido = true;
                                    break;
                                }
                            }
                            if (invalido)
                            {
                                realizaHistoricoTarefa(tarefaSelecionadaUsuario, 1);
                                result[0] = "invalido";
                                result[1] = "Existe alguma tarefa que deve ser executada antes da tarefa escolhida."; // causa do erro
                                result[2] = "Realize o check de todas as tarefas precedentes desta tarefa escolhida."; // solucao do erro
                                return result;
                                //return false;
                            }
                        }

                        #endregion

                        #region Logica de sucessao

                        int ultiIdTarefaExecutado = ultimoCheck.IdTarefa;
                        List<TarefaSucedente> listaSucedentes = TarefaSucedenteNG.getTarefasSucessorasCheck(ultiIdTarefaExecutado);
                        string nomeTarefasSucessoras = getNomeTarefasSucedentes(listaSucedentes);
                        foreach (TarefaSucedente proximaTarefa in listaSucedentes)
                        {
                            if (proximaTarefa.IdTarefaProxima != tarefaDaTag.IdTarefa)
                            {
                                // a tarefa do check eh uma sucessora da ultima tarefa checada
                                //break;
                                realizaHistoricoTarefa(tarefaSelecionadaUsuario, 1);
                                result[0] = "invalido";
                                result[1] = "Não é a vez da tarefa escolhida."; // causa do erro
                                result[2] = "Você deve escolher alguma(s) dessa(s) tarefa(s): " + nomeTarefasSucessoras + "."; // solucao do erro
                                return result;
                                //return false;
                            }
                        }

                        #endregion

                        //add na tabela de fluxo correto
                        AtividadeFluxoCorreto fluxoCorreto = new AtividadeFluxoCorreto();
                        fluxoCorreto.Ciclo = cicloAtual;
                        fluxoCorreto.IdTarefa = tarefaDaTag.IdTarefa;
                        fluxoCorreto.IdAtividade = ativ.Id;
                        fluxoCorreto.dataCheck = DateTime.Now;
                        AtividadeFluxoCorretoNG.addFluxoCorreto(fluxoCorreto);
                        TarefaNG.updateStatusExecucao(2, tarefaDaTag.IdTarefa);

                        result[0] = "valido";
                        result[1] = "Ok! Pode executar a proxima tarefa!"; // proximoPasso
                        result[2] = "";

                        if (tarefaDaTag.FinalizaFluxo)
                        {

                            if (ativ.NumMaximoCiclo != 0 && listFluxos.Count == ativ.NumMaximoCiclo)
                            {

                                int novoCiclo = cicloAtual + 1;
                                //update na atividade coluna cicloAtual com o novo valor
                                AtividadeNG.updateCicloAtualAtividade(novoCiclo, ativ.Id);
                                TarefaNG.updateStatusExecucaoByIdAtividade(1, ativ.Id);

                                result[0] = "valido";
                                result[1] = "Muito bem! Você finalizou o fluxo, o número de fluxos completos diários, foi atingido. Realize o check no próximo dia válido"; // proximoPasso
                                result[2] = "";
                            }
                            else
                            {
                                int novoCiclo = cicloAtual + 1;
                                //update na atividade coluna cicloAtual com o novo valor
                                AtividadeNG.updateCicloAtualAtividade(novoCiclo, ativ.Id);
                                TarefaNG.updateStatusExecucaoByIdAtividade(1, ativ.Id);

                                result[0] = "valido";
                                result[1] = "Muito bem! Você finalizou o fluxo, será iniciado um novo ciclo de execução da atividade."; // proximoPasso
                                result[2] = "";
                            }
                        }
                        realizaHistoricoTarefa(tarefaSelecionadaUsuario, 2);
                    }
                }
                else // CHECK SEQUENCIAL
                {
                    // TODO: NAO TEM CICLO    

                    //int cicloAtual = AtividadeNG.getCicloAtualAtividade(ativ.Id);
                    List<AtividadeFluxoCorreto> listFluxos = AtividadeFluxoCorretoNG.getAllCheckByIdAtividade(ativ.Id);

                    if (listFluxos == null || listFluxos.Count == 0) // nao tem nenhum registro de CHECK
                    {
                        if (tarefaDaTag.IniciaFluxo == false)
                        {
                            realizaHistoricoTarefa(tarefaSelecionadaUsuario, 1); // 1 = invalido
                            result[0] = "invalido";
                            result[1] = "A tarefa escolhida, não inicia a sua atividade."; // causa do erro
                            result[2] = "Realize o check de uma Tarefa que Inicie o fluxo da sua atividade."; // solucao do erro
                            return result;
                            //return false;
                        }
                        //add na tabela de fluxo correto
                        AtividadeFluxoCorreto fluxoCorreto = new AtividadeFluxoCorreto();
                        //fluxoCorreto.Ciclo = cicloAtual;
                        fluxoCorreto.IdTarefa = tarefaDaTag.IdTarefa;
                        fluxoCorreto.IdAtividade = ativ.Id;
                        fluxoCorreto.dataCheck = DateTime.Now;
                        AtividadeFluxoCorretoNG.addFluxoCorreto(fluxoCorreto);
                        TarefaNG.updateStatusExecucao(2, tarefaDaTag.IdTarefa);
                        AtividadeNG.updateStatusAtividade(ativ.Id, 2); // status = Em execucao
                        realizaHistoricoTarefa(tarefaSelecionadaUsuario, 2);

                        result[0] = "valido";
                        result[1] = "Ok! Pode executar a proxima tarefa!"; // proximoPasso
                        result[2] = "";
                        return result;
                    }
                    else // ja foi realizado algum check ...
                    {
                        AtividadeFluxoCorreto ultimoCheck = AtividadeFluxoCorretoNG.getUltimoCheckCorretoByIdAtividade(ativ.Id);

                        #region Logica de precedencia
                        List<TarefaPrecedente> listaAnte = TarefaPrecedenteNG.getTarefasAntecessorasCheck(tarefaDaTag.IdTarefa);
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

                        if (count != tamanhoListaAnte)
                        {
                            realizaHistoricoTarefa(tarefaSelecionadaUsuario, 1);
                            result[0] = "invalido";
                            result[1] = "Existe alguma tarefa que deve ser executada antes da tarefa escolhida."; // causa do erro
                            result[2] = "Realize o check de todas as tarefas precedentes desta tarefa escolhida."; // solucao do erro
                            return result;
                            //return false;
                        }

                        #endregion

                        #region Logica de sucessao

                        int ultiIdTarefaExecutado = ultimoCheck.IdTarefa;
                        List<TarefaSucedente> listaSucedentes = TarefaSucedenteNG.getTarefasSucessorasCheck(ultiIdTarefaExecutado);
                        string nomeTarefasSucessoras = getNomeTarefasSucedentes(listaSucedentes);
                        foreach (TarefaSucedente proximaTarefa in listaSucedentes)
                        {
                            if (proximaTarefa.IdTarefaProxima != tarefaDaTag.IdTarefa)
                            {
                                // a tarefa do check eh uma sucessora da ultima tarefa checada
                                //break;
                                realizaHistoricoTarefa(tarefaSelecionadaUsuario, 1);
                                result[0] = "invalido";
                                result[1] = "Não é a vez da tarefa escolhida."; // causa do erro
                                result[2] = "Você deve escolher alguma(s) dessa(s) tarefa(s): " + nomeTarefasSucessoras + "."; // solucao do erro
                                return result;
                                //return false;
                            }
                        }

                        #endregion

                        //add na tabela de fluxo correto
                        AtividadeFluxoCorreto fluxoCorreto = new AtividadeFluxoCorreto();
                        //fluxoCorreto.Ciclo = cicloAtual;
                        fluxoCorreto.IdTarefa = tarefaDaTag.IdTarefa;
                        fluxoCorreto.IdAtividade = ativ.Id;
                        fluxoCorreto.dataCheck = DateTime.Now;
                        AtividadeFluxoCorretoNG.addFluxoCorreto(fluxoCorreto);
                        TarefaNG.updateStatusExecucao(2, tarefaDaTag.IdTarefa);

                        result[0] = "valido";
                        result[1] = "Ok! Pode executar a proxima tarefa!"; // proximoPasso
                        result[2] = "";

                        if (tarefaDaTag.FinalizaFluxo)
                        {
                            //int novoCiclo = cicloAtual + 1;
                            //update na atividade coluna cicloAtual com o novo valor
                            //AtividadeNG.updateCicloAtualAtividade(novoCiclo, ativ.Id);
                            //TarefaNG.updateStatusExecucaoByIdAtividade(1, ativ.Id);


                            //update no status da atividade (PARA FINALIZADA)
                            AtividadeNG.updateStatusAtividade(ativ.Id, 3);
                            result[0] = "valido";
                            result[1] = "Muito bem! Você finalizou um fluxo completo da atividade. Essa tarefa chegou ao fim. "; // proximoPasso
                            result[2] = "";
                        }
                        realizaHistoricoTarefa(tarefaSelecionadaUsuario, 2);
                    }
                }
                //return true;
                return result;

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

        private static bool naoChegaNoTarget(int idTarefaChecada, int idTarefaAntecessora)
        {
            List<TarefaSucedente> listaSucessoras = TarefaSucedenteNG.getTarefasSucessorasCheck(idTarefaAntecessora);
            foreach (TarefaSucedente tarefaSucessora in listaSucessoras)
            {
                if (tarefaSucessora.IdTarefaProxima == idTarefaChecada)
                {
                    return true; // e ciclico 
                }
            }
            return false;
        }

        private static string getNomeTarefasSucedentes(List<TarefaSucedente> listaSucedentes)
        {
            string result = "";
            for (int i = 0; i <= listaSucedentes.Count - 1; i++)
            {
                TarefaSucedente tarefaSucedente = listaSucedentes[i];
                Tarefa tarefa = TarefaNG.getTarefa(tarefaSucedente.IdTarefaProxima);
                if (i != (listaSucedentes.Count - 1))
                {
                    result += tarefa.Nome + ", ";
                }
                else
                {
                    result += tarefa.Nome;
                }
            }
            return result;
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
                if (tarefa.IdTarefa != idTarefaTarget)
                {
                    listaIds.Add(Convert.ToString(tarefa.IdTarefa));
                }
            }

            return listaIds;
        }

        public static void realizaHistoricoTarefa(Tarefa tarefaSelecionadaUsuario, int idStatus)
        {
            try
            {
                //realizar historico dessa tarefa
                addRegistroCheckNFC(tarefaSelecionadaUsuario, idStatus);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao inserir o registro de Check: " + e.Message);
            }
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
                listaIds.Add(Convert.ToString(tarefa.IdTarefa));
            }

            return listaIds;
        }
        #endregion

    }
}
