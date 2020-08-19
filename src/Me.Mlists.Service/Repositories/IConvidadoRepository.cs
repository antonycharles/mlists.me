using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
{
    public interface IConvidadoRepository
    {
        Task<IList<Convidado>> GetAllConvidadosByListaIdAsync(string listaId);

        Task<IList<Convidado>> GetAllConvidadosByEmailAsync(string email);

        Task<Convidado> GetConvidadoById(string convidadoId);

        Task<Convidado> InsertConvidado(Convidado convidado);

        Task<Convidado> UpdateConvidadoStatusAsync(Convidado convidado);

        Task ExcluirConvidado(string convidadoId, string userId);
    }
}
