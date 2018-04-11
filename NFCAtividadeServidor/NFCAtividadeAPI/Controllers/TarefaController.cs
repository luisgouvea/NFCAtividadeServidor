using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NFCAtividadeAPI.Controllers
{
    public class TarefaController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage getTarefasByIdAtividade([FromBody]int idAtividade)
        {
            List<Tarefa> listaTarefas = Negocio.TarefaNG.getAllTarefasByIdAtividade(idAtividade);

            return Request.CreateResponse(HttpStatusCode.OK, listaTarefas);
        }

        [HttpPost]
        public HttpResponseMessage addTarefa([FromBody] Tarefa tarefa)
        {
            Boolean adicionado = Negocio.TarefaNG.addTarefa(tarefa);

            return Request.CreateResponse(HttpStatusCode.OK, adicionado);
        }

        [HttpPost]
        public HttpResponseMessage setarEncadeamentoTarefa([FromBody] Tarefa tarefa)
        {
            Boolean persistido = Negocio.TarefaNG.setarEncadeamentoTarefa(tarefa);

            return Request.CreateResponse(HttpStatusCode.OK, persistido);
        }
    }
}
