using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia.Modelos
{
    public class TAGEncadeamento
    {
        public int Id { get; set; }
        public int IdTagTarget { get; set; }
        public int IdTagAntecessora { get; set; }
    }
}
