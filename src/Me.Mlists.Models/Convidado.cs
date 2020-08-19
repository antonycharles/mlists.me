using System;
using System.Collections.Generic;
using System.Text;

namespace Me.Mlists.Models
{
    public class Convidado
    {
        public string Id { get; private set; }
        public string ListaId { get; private set; }
        public string UserId { get; private set; }
        public string ConvidadoPorId { get; private set; }
        public Lista Lista { get; private set; }
        public string EmailConvite { get; private set; }
        public DateTime DataEnvio { get; private set; }
        public DateTime? DataResposta { get; private set; }
        public bool? IsAceitou { get; private set; }

        public Convidado(string listaId, string emailConvite, string convidadoPorId, DateTime dataEnvio)
        {
            ListaId = listaId;
            EmailConvite = emailConvite;
            ConvidadoPorId = convidadoPorId;
            DataEnvio = dataEnvio;
        }

        public Convidado(string id, string userId, bool isAceitou)
        {
            Id = id;
            UserId = userId;
            IsAceitou = isAceitou; 
        }

        public void AlterarStatusConvidado(Convidado convidado)
        {
            IsAceitou = convidado.IsAceitou;
            DataResposta = DateTime.Now;
        }
    }
}
