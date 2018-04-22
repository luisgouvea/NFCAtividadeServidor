using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NFCAtividadeAPI.Controllers
{
    public class TarefaHistoricoCheckController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage realizarCheck(int idTagCheck, int idTarefa)
        {
            try
            {
                bool checkCorreto = Negocio.TarefaHistoricoCheckNG.realizarCheck(idTagCheck, idTarefa);
                return Request.CreateResponse(HttpStatusCode.OK, checkCorreto);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro: " + e.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage getAllRegistroCheckNFC([FromBody]int idAtividade)
        {
            try
            {
                List<TarefaHistoricoCheck> listaCheck = Negocio.TarefaHistoricoCheckNG.getAllRegistroCheckNFCByIdsTarefa(idAtividade);
                return Request.CreateResponse(HttpStatusCode.OK, listaCheck);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro: " + e.Message);
            }
        }
    }
}
