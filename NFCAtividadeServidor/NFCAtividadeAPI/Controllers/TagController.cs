using Persistencia.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NFCAtividadeAPI.Controllers
{
    public class TagController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage getInfRoteiroExecucao()
        {
            int idAtividade = 2;

            List<TAG> listaTags = Negocio.TagNG.getTagsByAtividade(idAtividade);

            return Request.CreateResponse(HttpStatusCode.OK, listaTags);
        }

        [HttpPost]
        public HttpResponseMessage getInfRoteiroExecucao([FromBody]List<int> parametros)
        {
            int idAtividade = parametros[0];

            List<TAG> listaTags = Negocio.TagNG.getTagsByAtividade(idAtividade);
            
            return Request.CreateResponse(HttpStatusCode.OK, listaTags);
        }
    }
}
