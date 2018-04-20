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
                Boolean persistido = Negocio.TarefaPrecedenteNG.setarPrecedenciaTarefa(tarefa);

                return Request.CreateResponse(HttpStatusCode.OK, persistido);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro: " + e.Message);
            }
        }
    }
}
