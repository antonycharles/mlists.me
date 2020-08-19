using Me.Mlists.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Me.Mlists.Models
{
    public class Participante
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser User { get; set; }
        public string ListaId { get; private set; }
        public Lista Lista { get; private set; }
        public ParticipantePerfilEnum ParticipantePerfil { get; private set; }
        public IList<Tarefa> Tarefas { get; private set; }

        public Participante()
        {

        }

        public Participante(string userId, Lista lista)
        {
            UserId = userId;
            Lista = lista;
            ListaId = lista.Id;
            ParticipantePerfil = ParticipantePerfilEnum.Administrador;
        }

        public Participante(string userId, string listaId, ParticipantePerfilEnum participantePerfil)
        {
            UserId = userId;
            ListaId = listaId;
            ParticipantePerfil = participantePerfil;
        }

        public Participante(int id, ParticipantePerfilEnum participantePerfilEnum)
        {
            Id = id;
            ParticipantePerfil = participantePerfilEnum;
        }

        public void UpdatePerfil(Participante participante)
        {
            this.ParticipantePerfil = participante.ParticipantePerfil;
        }
    }
}
