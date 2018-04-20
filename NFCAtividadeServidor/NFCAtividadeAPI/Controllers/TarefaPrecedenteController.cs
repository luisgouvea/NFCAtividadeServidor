using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NFCAtividadeAPI.Controllers
{
    public class TarefaPrecedenteController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage setarPrecedenciaTarefa([FromBody] Tarefa tarefa)
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro: " + e.Message);
            }
        }
    }
}
