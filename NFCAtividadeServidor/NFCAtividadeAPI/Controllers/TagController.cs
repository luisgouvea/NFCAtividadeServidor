using System;
using Persistencia.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NFCAtividadeAPI.Controllers
{
    public class TagController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage addTag([FromBody] TAG tag)
        {
            Boolean adicionado = Negocio.TagNG.addTag(tag);

            return Request.CreateResponse(HttpStatusCode.OK, adicionado);
        }

        [HttpPost]
        public HttpResponseMessage getTagsByIdUsuario([FromBody]int idUsuario)
        {
            List<TAG> listaTags = Negocio.TagNG.getAllTagsByIdUsuario(idUsuario);

            return Request.CreateResponse(HttpStatusCode.OK, listaTags);
        }
    }
}
