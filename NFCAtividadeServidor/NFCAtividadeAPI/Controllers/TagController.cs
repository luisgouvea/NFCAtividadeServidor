using System;
using Persistencia.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia.ModelosErro;

namespace NFCAtividadeAPI.Controllers
{
    public class TagController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage addTag([FromBody] TAG tag)
        {
            try
            {
                Boolean adicionado = Negocio.TagNG.addTag(tag);
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

        [HttpPost]
        public HttpResponseMessage getTagsByIdUsuario([FromBody]int idUsuario)
        {
            try
            {
                List<TAG> listaTags = Negocio.TagNG.getAllTagsByIdUsuario(idUsuario);
                return Request.CreateResponse(HttpStatusCode.OK, listaTags);
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
        public HttpResponseMessage getDetalhesTag([FromBody]int idTarefa)
        {
            try
            {
                Tarefa tarefa = Negocio.TarefaNG.getTarefa(idTarefa);
                TAG tag = Negocio.TagNG.getTag(tarefa.IdentificadorTag);
                return Request.CreateResponse(HttpStatusCode.OK, tag);
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
