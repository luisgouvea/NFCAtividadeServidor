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
                APIError erro = new APIError();
                erro.statusCode = "400";
                erro.message = "Ocorreu um erro: " + e.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro);
            }
        }

        [HttpPost]
        public HttpResponseMessage getTarefasRoteiroByIdAtividade([FromBody]int idAtividade)
        {
            try
            {
                List<Tarefa> listaTarefas = Negocio.TarefaNG.getAllTarefasRoteiroByIdAtividade(idAtividade);

                return Request.CreateResponse(HttpStatusCode.OK, listaTarefas);
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
        public HttpResponseMessage addTarefa([FromBody] Tarefa tarefa)
        {
            try
            {
                Boolean adicionado = Negocio.TarefaNG.addTarefa(tarefa);

                return Request.CreateResponse(HttpStatusCode.OK, adicionado);
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
