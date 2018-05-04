using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.ModelosUtil
{
    public class FiltroPesquisaHome
    {
        public int IdStatusAtividade { get; set; }
        public DateTime DataCriacao { get; set; }
        public string DescricaoAtividade { get; set; }
        public int IdUsuario { get; set; }
    }
}
