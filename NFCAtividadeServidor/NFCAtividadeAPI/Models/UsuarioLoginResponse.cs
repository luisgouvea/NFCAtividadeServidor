using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFCAtividadeAPI.Models
{
    public class UsuarioLoginResponse
    {
        private string id;  // the name field
        public string Id    // the Name property
        {
            get
            {
                return id;
            }
            set { id = value; }
        }
    }
}