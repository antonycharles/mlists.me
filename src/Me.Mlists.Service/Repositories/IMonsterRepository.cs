using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
{
    public interface IMonsterRepository
    {
        int CountListaMonsters();
        void SetListaMonsters(List<Monster> monsters);

        Task<Monster> GetMonsterAleatorio();

        Task<IList<Monster>> GetMonsters();
    }
}
