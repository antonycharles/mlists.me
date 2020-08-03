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
    public class ListaSecaoRepository : BaseRepository, IListaSecaoRepository
    {
        public ListaSecaoRepository(
            ApplicationContext cont) : base(cont)
        {
        }

        public async Task<IList<ListaSecao>> GetListaSecoesByListaIdAsync(string listaId)
        {
            return await context.ListaSecoes
                            .Where(w => w.ListaId == listaId)
                            .OrderBy(o => o.Descricao)
                            .ToListAsync();
        }
    }
}
