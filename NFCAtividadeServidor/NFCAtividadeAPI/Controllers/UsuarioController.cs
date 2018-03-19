using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia.Modelos;

namespace NFCAtividadeAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage LogarUsuario([FromBody]Dictionary<String, String> parametros)
        {
            try
            {
                string login = parametros["login"];
                string senha = parametros["senha"];
                Usuario usuario = Negocio.UsuarioNG.getUsuario(login, senha);

                return Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
