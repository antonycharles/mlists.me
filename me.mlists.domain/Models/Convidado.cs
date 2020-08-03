using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.domain.Models
{
    public class Convidado
    {
        public string Id { get; private set; }
        public string ListaId { get; private set; }
        public Lista Lista { get; private set; }
        public string EmailConvite { get; private set; }
    }
}
