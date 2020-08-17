using me.mlists.domain.Enums;
using me.mlists.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace me.mlists.web.Areas.Painel.ViewModels
{
    public class ParticipanteFormViewModel
    {
        public int ParticipanteId { get; set; }

        public ParticipantePerfilEnum ParticipantePerfil { get; set; }

        public IList<Participante> participantes { get; set; }

        public Participante ToParticipanteUpdate()
        {
            return new Participante(this.ParticipanteId, this.ParticipantePerfil);
        }
    }
}
