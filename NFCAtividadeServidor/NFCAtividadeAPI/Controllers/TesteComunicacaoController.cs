using NFCAtividadeAPI.Models;
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
        
        [HttpPost]
        public HttpResponseMessage Comunicar([FromBody]Dictionary<String,String> parametros)
        {
            string login = parametros["login"];
            string senha = parametros["senha"];
            //TODO: getUsuario
             var json = new UsuarioLoginResponse
            {
                Id = "1"
            };
            
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
    }
}
