using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
{
    public interface IParticipanteRepository
    {
        Task<Participante> GetParticipanteByIdAndListaId(string listaId, string userId);

        Task<IList<Participante>> GetParticipanteAllByIdAndListaId(string listaId, string userId);

        Task<Participante> UpdatePerfilParticipanteAsync(Participante participante, string userId);
    }
}
