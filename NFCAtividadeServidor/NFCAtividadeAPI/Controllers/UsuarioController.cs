using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia.Modelos;
using Persistencia.ModelosErro;

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

                return Request.CreateResponse(HttpStatusCode.OK, usuario.IdUsuario);
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
        public HttpResponseMessage CriarConta(Usuario usuario)
        {
            try
            {
                int idUsuario = Negocio.UsuarioNG.addUsuario(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, idUsuario);
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
        public HttpResponseMessage ListAllUsuarioAddAtivVincExecutor([FromBody]int idUsuarioTarget)
        {
            try
            {
                List<Usuario> listUsuario = Negocio.UsuarioNG.listAllUsuarioAddAtivVincExecutor(idUsuarioTarget);

                return Request.CreateResponse(HttpStatusCode.OK, listUsuario);
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
