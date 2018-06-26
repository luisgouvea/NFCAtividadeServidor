using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Modelos;

namespace Negocio
{
    public class AtividadeFluxoCorretoNG
    {
        public static AtividadeFluxoCorreto getUltimoCheckCorretoByIdAtividade(int idAtividade)
        {
            return Persistencia.AtividadeFluxoCorretoDD.getUltimoCheckCorretoByIdAtividade(idAtividade);
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByIdAtividade(int idAtividade)
        {
            return Persistencia.AtividadeFluxoCorretoDD.getAllCheckByIdAtividade(idAtividade);
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByCicloAndDayCheckAndIdAtividade(DateTime dataCheck, int ciclo, int idAtividade)
        {
            return Persistencia.AtividadeFluxoCorretoDD.getAllCheckByCicloAndDayCheckAndIdAtividade(dataCheck, ciclo, idAtividade);
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByMonthCheckAndIdAtividade(DateTime dataCheck, int idAtividade)
        {
            return Persistencia.AtividadeFluxoCorretoDD.getAllCheckByMonthCheckAndIdAtividade(dataCheck, idAtividade);
        }

        public static List<AtividadeFluxoCorreto> getAllCheckByCicloAndIdAtividade(int ciclo, int idAtividade)
        {
            return Persistencia.AtividadeFluxoCorretoDD.getAllCheckByCicloAndIdAtividade(ciclo, idAtividade);
        }

        public static Boolean addFluxoCorreto(AtividadeFluxoCorreto fluxoCorreto)
        {
            return Persistencia.AtividadeFluxoCorretoDD.addFluxoCorreto(fluxoCorreto);
        }
    }
}
