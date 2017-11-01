using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFCAtividadeAPI.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<TAG> listTag { get; set; }
    }
}