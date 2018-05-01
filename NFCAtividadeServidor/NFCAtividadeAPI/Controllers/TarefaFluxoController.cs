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
    public class TarefaFluxoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage setarFluxoTarefa([FromBody] Tarefa tarefa)
        {
            try
            {
                Boolean persistidoPrecedencia = Negocio.TarefaPrecedenteNG.setarPrecedenciaTarefa(tarefa);
                Boolean persistidoSucessao = Negocio.TarefaSucedenteNG.setarSucessaoTarefa(tarefa);
                if(persistidoPrecedencia && persistidoSucessao)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                return Request.CreateResponse(HttpStatusCode.OK, false);
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
