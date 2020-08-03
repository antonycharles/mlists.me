using me.mlists.domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace me.mlists.domain.Models
{
    public class Participante
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }
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
    }
}
