using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
{
    public interface IListaSecaoRepository
    {
        Task<IList<ListaSecao>> GetListaSecoesByListaIdAsync(string listaId);
    }
}
