using me.mlists.data.Data;
using me.mlists.domain.Enums;
using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace me.mlists.service.Repositories
{
    public class TarefeRepository : BaseRepository, ITarefaRepository
    {
        private readonly IParticipanteRepository _participanteRepository;
        public TarefeRepository(
            ApplicationContext cont,
            IParticipanteRepository participanteRepository) : base(cont)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<Tarefa> InsertTarefaAsync(Tarefa tarefa, string userId)
        {
            var participante = await _participanteRepository.GetParticipanteByIdAndListaId(tarefa.ListaId, userId);
            tarefa.setParticipante(participante);

            context.Add(tarefa);
            await context.SaveChangesAsync();

            return tarefa;
        }

        public async Task<Tarefa> UpdateTarefaAsync(Tarefa tarefa, string userId)
        {
            var tarefaOriginal = await context.Tarefas
                                        .Where(
                                            w => w.Lista.Participantes.Any(f => f.UserId == userId && f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado) &&
                                            w.Id == tarefa.Id)
                                        .SingleOrDefaultAsync();

            if (tarefaOriginal == null)
                throw new ArgumentException("Alteração não realizada!");

            tarefaOriginal.Update(tarefa);
            await context.SaveChangesAsync();

            return tarefaOriginal;
        }

        public async Task UpdateTarefaCheckedTrueAsync(int tarefaId, string userId)
        {
            var tarefa = await context.Tarefas
                                        .Where(
                                            w => w.Lista.Participantes.Any(f => f.UserId == userId && f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado) &&
                                            w.Id == tarefaId)
                                        .SingleOrDefaultAsync();

            if (tarefa == null)
                throw new ArgumentException("Alteração não realizada!");

            tarefa.setTarefaCheckedTrue();
            await context.SaveChangesAsync();
        }

        public async Task UpdateTarefaLixeiraTrueAsync(int tarefaId, string userId)
        {
            var tarefa = await context.Tarefas
                                        .Where(
                                            w => w.Lista.Participantes.Any(f => f.UserId == userId && f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado) &&
                                            w.Id == tarefaId)
                                        .SingleOrDefaultAsync();

            if (tarefa == null)
                throw new ArgumentException("Alteração não realizada!");

            tarefa.setLixeiraTrue();
            await context.SaveChangesAsync();
        }
    }
}
