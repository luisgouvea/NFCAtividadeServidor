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
    public class NotificacaoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage addNotificacao([FromBody] NotificacaoUsuario notificacao)
        {
            try
            {
                Boolean adicionado = Negocio.NotificacaoUsuarioNG.addNotificacao(notificacao);
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
        public HttpResponseMessage getNotificacoesByUsuario([FromBody] int idUsuario)
        {
            try
            {
                List<NotificacaoUsuario> listaNotificacoes = Negocio.NotificacaoUsuarioNG.getNotificacoesByUsuario(idUsuario);
                return Request.CreateResponse(HttpStatusCode.OK, listaNotificacoes);
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
