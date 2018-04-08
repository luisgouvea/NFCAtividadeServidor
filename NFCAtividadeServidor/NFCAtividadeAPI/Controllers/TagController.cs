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
        [HttpPost]
        public HttpResponseMessage getTagsByIdAtividade([FromBody]int idAtividade)
        {
            List<TAG> listaTags = Negocio.TagNG.getAllTagsByIdAtividade(idAtividade);

            return Request.CreateResponse(HttpStatusCode.OK, listaTags);
        }

        [HttpPost]
        public HttpResponseMessage addTag([FromBody] TAG tag)
        {
            //TAG tag = Newtonsoft.Json.JsonConvert.DeserializeObject<TAG>(infToAddTag[1]);
            Boolean adicionado = Negocio.TagNG.addTag(tag);

            return Request.CreateResponse(HttpStatusCode.OK, adicionado);
        }

        [HttpPost]
        public HttpResponseMessage setarEncadeamentoTag([FromBody] TAG tag)
        {
            Boolean persistido = Negocio.TagNG.setarEncadeamentoTag(tag);

            return Request.CreateResponse(HttpStatusCode.OK, persistido);
        }
    }
}
