using me.mlists.domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace me.mlists.service.Repositories
{
    public interface IListaRepository
    {
        Task<IList<Lista>> GetAllListasByUserAsync(string userId,bool? isLixeira);

        Task<Lista> GetListaByIdAsync(string listaId, string userId);

        Task<Lista> GetListaByIdAndAtivoAsync(string listaId, string userId);

        Task<Lista> InsertListaAsync(Lista lista);

        Task<Lista> UpdateListaAsync(Lista lista, string userId);

        Task UpdateListaStatusLixeiraAsync(string listaId, string userId);
        Task UpdateListaRestaurarStatusAtivoAsync(string listaId, string userId);
        Task ExcluirPermanenteListaAsync(string listaId, string userId);

    }
}
