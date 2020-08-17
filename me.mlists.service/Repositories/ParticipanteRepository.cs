using me.mlists.data.Data;
using me.mlists.domain.Enums;
using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace me.mlists.service.Repositories
{
    public class ParticipanteRepository : BaseRepository, IParticipanteRepository
    {
        public ParticipanteRepository(
            ApplicationContext cont) : base(cont)
        {
        }

        public async Task<Participante> GetParticipanteByIdAndListaId(string listaId, string userId)
        {
            return await context.Participantes
                                    .Where(w => w.ListaId == listaId &&
                                        w.UserId == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<IList<Participante>> GetParticipanteAllByIdAndListaId(string listaId, string userId)
        {
            return await context.Participantes
                                    .Where(w => w.ListaId == listaId &&
                                        w.Lista.Participantes.Any(
                                                 f => f.UserId == userId &&
                                                f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado))
                                    .ToListAsync();
        }

        public async Task<Participante> UpdatePerfilParticipanteAsync(Participante participante, string userId)
        {
            var resultado = await context.Participantes
                                        .Where(
                                            w => w.Id == participante.Id &&
                                            w.Lista.Participantes.Any(
                                                 f => f.UserId == userId &&
                                                f.ParticipantePerfil == ParticipantePerfilEnum.Administrador))
                                        .SingleOrDefaultAsync();

            if (resultado == null)
                throw new ArgumentException("Alteração não realizada!");

            if (resultado.ParticipantePerfil == ParticipantePerfilEnum.Administrador)
            {
                if (await this.CountTotalParticipantesAdminsByListaId(resultado.ListaId) == 1)
                    throw new ArgumentException("Toda lista tem que possuir pelo menos um Administrador!");
            }

            resultado.UpdatePerfil(participante);

            await context.SaveChangesAsync();

            return resultado;
        }

        private async Task<int> CountTotalParticipantesAdminsByListaId(string listaId)
        {
             return await context.Participantes
                                        .Where(w => w.ListaId == listaId && w.ParticipantePerfil == ParticipantePerfilEnum.Administrador)
                                        .CountAsync();
        }
    }
}
