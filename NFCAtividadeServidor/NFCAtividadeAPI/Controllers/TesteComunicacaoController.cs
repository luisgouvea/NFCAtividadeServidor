using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NFCAtividadeAPI.Controllers
{
    public class TesteComunicacaoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Comunicar()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Funcionou a comunicacao");
        }
    }
}
