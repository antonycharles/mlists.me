using me.mlists.domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace me.mlists.service.Repositories
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
