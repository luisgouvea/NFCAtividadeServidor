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
            try
            {
                List<Tarefa> listaTarefas = Negocio.TarefaNG.getAllTarefasByIdAtividade(idAtividade);

                return Request.CreateResponse(HttpStatusCode.OK, listaTarefas);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro: " + e.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage addTarefa([FromBody] Tarefa tarefa)
        {
            try
            {
                Boolean adicionado = Negocio.TarefaNG.addTarefa(tarefa);

                return Request.CreateResponse(HttpStatusCode.OK, adicionado);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro: " + e.Message);
            }
        }
    }
}
