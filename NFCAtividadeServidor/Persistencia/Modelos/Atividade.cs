using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int IdStatus { get; set; }
        public int IdUsuarioCriador { get; set; }
        public int IdUsuarioExecutor { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int CicloAtual { get; set; }
        public List<Tarefa> listTarefa { get; set; }
    }
}
