using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Modelos
{
    public class AmigoUsuario : Usuario
    {
        public int IdAmigoUsuario { get; set; } // PK da tabela
        // OBS: IdUsuarioTarget eh a mesma coisa que o IdUsuario do modelo Usuario

        // Id do usuario que eu quero adicionar a os amigos
        public int IdUsuarioAmigo { get; set; }
    }
}
