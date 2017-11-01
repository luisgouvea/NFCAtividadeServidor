using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFCAtividadeAPI.Models
{
    public class TAG
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<string> listAntecessores { get; set; }
    }
}