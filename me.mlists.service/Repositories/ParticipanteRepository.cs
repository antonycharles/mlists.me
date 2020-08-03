using me.mlists.data.Data;
using me.mlists.domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
