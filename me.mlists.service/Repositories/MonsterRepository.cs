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
    public class MonsterRepository : BaseRepository, IMonsterRepository
    {
        public MonsterRepository(ApplicationContext cont) : base(cont)
        {
        }

        public int CountListaMonsters()
        {
            return context.Monsters.Count();
        }

        public async Task<Monster> GetMonsterAleatorio()
        {
            var rand = new Random();
            return await context.Monsters.Skip(rand.Next(1, context.Monsters.Count())).FirstAsync();
        }

        public async Task<IList<Monster>> GetMonsters()
        {
            return await context.Monsters.ToListAsync();
        }

        public void SetListaMonsters(List<Monster> monsters)
        {
            foreach(var monster in monsters)
            {
                context.Monsters.Add(monster);
            }

            context.SaveChanges();
        }
    }
}
