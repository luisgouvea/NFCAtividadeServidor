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
    public class NotificacaoAddAtividadeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage addNotificacaoAddAtividade([FromBody] NotificacaoUsuarioAddAtividade notificacaoAddAtividade)
        {
            try
            {
                Boolean adicionado = Negocio.NotificacaoUsuarioAddAtividadeNG.addNotificacaoAddAtividade(notificacaoAddAtividade);
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
