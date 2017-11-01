using NFCAtividadeAPI.Models;
using System;
using System.Collections;
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
        public HttpResponseMessage Comunicar([FromBody]Dictionary<String, String> parametros)
        {
            string login = parametros["login"];
            string senha = parametros["senha"];
            //TODO: getUsuario
            var json = new Atividade
            {
                Id = 1
            };

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [HttpPost]
        public HttpResponseMessage getAtivExecutar([FromBody]List<String> parametros)
        {
            //TODO: getUsuario
            String idUsuario = parametros[0];

            List<TAG> listaTagAtiv1 = new List<TAG>
            {
                new TAG {Id = 1, Nome = "TAG_B", listAntecessores = new List<string> {"A->B","D->B"}},
                new TAG {Id = 2, Nome = "TAG_D", listAntecessores = new List<string> {"C->D","E->D"} }
            };

            List<TAG> listaTagAtiv2 = new List<TAG>
            {
                new TAG {Id = 3, Nome = "TAG_C", listAntecessores = new List<string> {"A->C","D->C"}},
                new TAG {Id = 4, Nome = "TAG_E", listAntecessores = new List<string> {"C->E","F->E"} }
            };

            List<Atividade> listaAtividades = new List<Atividade>
            {
                new Atividade { Id = 1, Nome = "Atividade_1", listTag = listaTagAtiv1},
                new Atividade { Id = 2, Nome = "Atividade_2", listTag = listaTagAtiv2}
            };
                        
            return Request.CreateResponse(HttpStatusCode.OK, listaAtividades);
        }
    }
}
