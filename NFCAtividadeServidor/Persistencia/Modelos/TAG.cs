using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class TAG
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCheck { get; set; }
        public string Comentario { get; set; }
        public List<string> listAntecessores { get; set; }
    }
}
