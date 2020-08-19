using Me.Mlists.Models.Enums;
using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Painel.ViewModels
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
