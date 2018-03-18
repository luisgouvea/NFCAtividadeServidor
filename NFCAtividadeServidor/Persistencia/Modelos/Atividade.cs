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
        public List<TAG> listTag { get; set; }
    }
}
