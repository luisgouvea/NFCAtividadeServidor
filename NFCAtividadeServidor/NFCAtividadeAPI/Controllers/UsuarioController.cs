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
        public HttpResponseMessage LogarUsuario([FromBody]List<String> parametros)
        {
            try
            {
                //string login = parametros["login"];
                //string senha = parametros["senha"];
                string login = parametros[0];
                string senha = parametros[1];
                Usuario usuario = Negocio.UsuarioNG.getUsuario(login, senha);

                return Request.CreateResponse(HttpStatusCode.OK, usuario.Id);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage ListAllUsuarioAddAtivVincExecutor(int idUsuarioTarget)
        {
            try
            {
                List<Usuario> listUsuario = Negocio.UsuarioNG.listAllUsuarioAddAtivVincExecutor(idUsuarioTarget);

                return Request.CreateResponse(HttpStatusCode.OK, listUsuario);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
