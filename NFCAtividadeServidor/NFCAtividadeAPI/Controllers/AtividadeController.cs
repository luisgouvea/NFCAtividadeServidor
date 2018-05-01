using Persistencia.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Persistencia.ModelosErro;

namespace NFCAtividadeAPI.Controllers
{
    public class AtividadeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage getAtivExecutar([FromBody]int idUsuario)
        {
            //TODO: getUsuario
            //String idUsuario = parametros[0];
            List<Atividade> listaAtividades = Negocio.AtividadeNG.getAllAtivExecutar(idUsuario);

            return Request.CreateResponse(HttpStatusCode.OK, listaAtividades);
        }

        [HttpPost]
        public HttpResponseMessage getAtivAdicionadas([FromBody]int idUsuario)
        {
            List<Atividade> listaAtividades = Negocio.AtividadeNG.getAllAtivAdicionadas(idUsuario);

            return Request.CreateResponse(HttpStatusCode.OK, listaAtividades);
        }

        [HttpPost]
        public HttpResponseMessage criarAtividade([FromBody] Atividade ativ)
        {
            try
            {
                ativ.IdStatus = 1;
                Boolean adicionado = Negocio.AtividadeNG.adicionarAtividade(ativ);
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
