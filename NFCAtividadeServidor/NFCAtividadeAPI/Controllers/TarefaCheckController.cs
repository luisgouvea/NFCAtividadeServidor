using Persistencia.Modelos;
using Persistencia.ModelosErro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NFCAtividadeAPI.Controllers
{
    public class TarefaCheckController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage realizarCheck(string identificadorTagCheck, int idTarefa)
        {
            try
            {
                string [] resultCheck = Negocio.TarefaCheckNG.realizarCheck(identificadorTagCheck, idTarefa);
                return Request.CreateResponse(HttpStatusCode.OK, resultCheck);
            }
            catch (Exception e)
            {
                APIError erro = new APIError();
                erro.statusCode = "400";
                erro.message = "Ocorreu um erro: " + e.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro);
            }
        }

        [HttpPost]
        public HttpResponseMessage getAllRegistroCheckNFC([FromBody]int idAtividade)
        {
            try
            {
                List<TarefaCheck> listaCheck = Negocio.TarefaCheckNG.getAllRegistroCheckNFCByIdsTarefa(idAtividade);
                return Request.CreateResponse(HttpStatusCode.OK, listaCheck);
            }
            catch (Exception e)
            {
                APIError erro = new APIError();
                erro.statusCode = "400";
                erro.message = "Ocorreu um erro: " + e.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro);
            }
        }
    }
}
