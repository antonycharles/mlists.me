using Me.Mlists.Data.Data;
using Me.Mlists.Models.Enums;
using Me.Mlists.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
{
    public class ConvidadoRepository : BaseRepository, IConvidadoRepository
    {

        public ConvidadoRepository(ApplicationContext cont) : base(cont)
        {
        }

        public async Task<IList<Convidado>> GetAllConvidadosByEmailAsync(string email)
        {
            return await context.Convidados
                            .Include(f => f.Lista)
                            .Where(w => w.EmailConvite == email && w.IsAceitou == null)
                            .ToListAsync();
        }

        public async Task<IList<Convidado>> GetAllConvidadosByListaIdAsync(string listaId)
        {
            return await context.Convidados
                            .Include(f => f.Lista)
                            .Where(w => w.ListaId == listaId)
                            .ToListAsync();
        }

        public async Task<Convidado> GetConvidadoById(string convidadoId)
        {
            return await context.Convidados
                            .Include(f => f.Lista)
                            .Where(w => w.Id == convidadoId)
                            .FirstOrDefaultAsync();
        }

        public async Task<Convidado> InsertConvidado(Convidado convidado)
        {
            context.Add(convidado);
            await context.SaveChangesAsync();

            return convidado;
        }

        public async Task<Convidado> UpdateConvidadoStatusAsync(Convidado convidado)
        {
            var resultado = await context.Convidados
                                   .Where(w => w.Id == convidado.Id)
                                   .SingleOrDefaultAsync();

            if (resultado == null)
                throw new ArgumentException("Alteração não realizada!");

            resultado.AlterarStatusConvidado(convidado);

            if (resultado.IsAceitou == true) { 
                var participante = new Participante(convidado.UserId, resultado.ListaId, ParticipantePerfilEnum.Participante);
                context.Add(participante);
            }

            await context.SaveChangesAsync();

            return resultado;
        }

        public async Task ExcluirConvidado(string convidadoId, string userId)
        {
            var convidado = await context.Convidados
                                .Where(
                                    w => w.Lista.Participantes.Any(
                                        f => f.UserId == userId &&
                                        f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado) &&
                                    w.Id == convidadoId)
                                .SingleOrDefaultAsync();

            if (convidado == null)
                throw new ArgumentException("Alteração não realizada!");

            context.Remove(convidado);
            await context.SaveChangesAsync();
        }
    }
}
