using Persistencia.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace NFCAtividadeAPI.Controllers
{
    public class AtividadeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage getAtivExecutar([FromBody]String idUsuario)
        {
            //TODO: getUsuario
            //String idUsuario = parametros[0];
            //List<TAG> li = List<TAG>(parametros[1]);
            List<Atividade> listaAtividades = Negocio.AtividadeNG.getAllAtivExecutar(idUsuario);

            //foreach (Atividade ativ in listaAtividades)
            //{
            //    int id = ativ.Id;
            //    List<TAG> listaTags = Negocio.TagNG.getTagsByAtividade(id);
            //}


            //List<TAG> listaTagAtiv1 = new List<TAG>
            //{
            //    new TAG {Id = 1, Nome = "TAG_B", listAntecessores = new List<string> {"A->B","D->B"}},
            //    new TAG {Id = 2, Nome = "TAG_D", listAntecessores = new List<string> {"C->D","E->D"} }
            //};

            //List<TAG> listaTagAtiv2 = new List<TAG>
            //{
            //    new TAG {Id = 3, Nome = "TAG_C", listAntecessores = new List<string> {"A->C","D->C"}},
            //    new TAG {Id = 4, Nome = "TAG_E", listAntecessores = new List<string> {"C->E","F->E"} }
            //};

            //List<Atividade> listaAtividades = new List<Atividade>
            //{
            //    new Atividade { Id = 1, Nome = "Atividade_1", listTag = listaTagAtiv1},
            //    new Atividade { Id = 2, Nome = "Atividade_2", listTag = listaTagAtiv2}
            //};

            return Request.CreateResponse(HttpStatusCode.OK, listaAtividades);
        }

        [HttpPost]
        public HttpResponseMessage getAtivAdicionadas([FromBody]String idUsuario)
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro: " + e.Message);
            }
        }
    }
}
