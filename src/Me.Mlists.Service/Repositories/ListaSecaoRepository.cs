using Me.Mlists.Data.Data;
using Me.Mlists.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
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
