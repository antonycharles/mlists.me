using Me.Mlists.Data.Data;
using Me.Mlists.Models.Enums;
using Me.Mlists.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
{
    public class ListaRepository : BaseRepository, IListaRepository
    {
        private readonly IMonsterRepository _monsterRepository;

        public ListaRepository(
            ApplicationContext cont, 
            IMonsterRepository monsterRepository) : base(cont)
        {
            _monsterRepository = monsterRepository;
        }

        public async Task<IList<Lista>> GetAllListasByUserAsync(string userId, bool? isLixeira = false)
        {
            var listaStatus = ListaStatusEnum.Ativo;

            if (isLixeira == true)
            {
                listaStatus = ListaStatusEnum.Lixeira;
            }

            return await context.Listas
                            .Include(f => f.Monster)
                            .Include(f => f.Tarefas)
                            .Include(f => f.Participantes)
                            .Where(
                                w => w.Participantes.Any(
                                    f => f.UserId == userId && 
                                    f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado) && 
                                w.ListaStatus == listaStatus)
                            .OrderBy(o => o.Nome)
                            .ToListAsync();
        }

        public async Task<Lista> GetListaByIdAsync(string listaId, string userId)
        {
            return await context.Listas
                            .Include(f => f.Monster)
                            .Include(f => f.Tarefas)
                            .Include(f => f.Participantes)
                            .Where(
                                w => w.Id == listaId && 
                                w.Participantes.Any(f => f.UserId == userId))
                            .FirstOrDefaultAsync();
        }

        public async Task<Lista> GetListaByIdAndAtivoAsync(string listaId, string userId)
        {
            return await context.Listas
                            .Include(f => f.Monster)
                            .Include(f => f.Tarefas)
                            .Include(f => f.Participantes)
                            .Where(
                                w => w.Id == listaId && 
                                w.Participantes.Any(
                                    f => f.UserId == userId && 
                                    f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado) && 
                                w.ListaStatus == ListaStatusEnum.Ativo)
                            .FirstOrDefaultAsync();
        }

        public async Task<Lista> InsertListaAsync(Lista lista)
        {
            var monster = await _monsterRepository.GetMonsterAleatorio();

            lista.setMonster(monster);
            context.Add(lista);

            var participante = new Participante(lista.CriadoPorId, lista);
            context.Add(participante);

            await context.SaveChangesAsync();

            return lista;
        }

        public async Task UpdateListaStatusLixeiraAsync(string listaId, string userId)
        {
            var lista = await context.Listas
                                        .Where(
                                            w => w.Participantes.Any(
                                                f => f.UserId == userId && 
                                                f.ParticipantePerfil == ParticipantePerfilEnum.Administrador) &&
                                            w.Id == listaId)
                                        .SingleOrDefaultAsync();
            if (lista == null)
                throw new ArgumentException("Alteração não realizada!");

            lista.setStatusEnumLixeira();
            await context.SaveChangesAsync();
        }

        public async Task<Lista> UpdateListaAsync(Lista lista, string userId)
        {
            var resultado = await context.Listas
                                        .Include(f => f.Monster)
                                        .Where(
                                            w => w.Participantes.Any(
                                                f => f.UserId == userId &&
                                                f.ParticipantePerfil != ParticipantePerfilEnum.Bloqueado) && 
                                            w.Id == lista.Id)
                                        .SingleOrDefaultAsync();
            if (resultado == null)
                throw new ArgumentException("Alteração não realizada!");

            resultado.Update(lista);
            
            if(resultado.Monster.Id != lista.MonsterId)
            {
                resultado.setMonster(context.Monsters.First(w => w.Id == resultado.MonsterId));
            }

            await context.SaveChangesAsync();

            return resultado;
        }

        public async Task UpdateListaRestaurarStatusAtivoAsync(string listaId, string userId)
        {
            var lista = await context.Listas
                                        .Where(
                                            w => w.Participantes.Any(
                                                f => f.UserId == userId &&
                                                f.ParticipantePerfil == ParticipantePerfilEnum.Administrador) &&
                                            w.Id == listaId)
                                        .SingleOrDefaultAsync();
            if (lista == null)
                throw new ArgumentException("Restauração não realizada!");

            lista.setStatusEnumAtivo();
            await context.SaveChangesAsync();
        }

        public async Task ExcluirPermanenteListaAsync(string listaId, string userId)
        {
            var lista = await context.Listas
                                    .Include(f => f.Tarefas)
                                    .Include(f => f.ListaSecaos)
                                    .Include(f => f.Participantes)
                                    .Where(
                                        w => w.Participantes.Any(
                                            f => f.UserId == userId &&
                                            f.ParticipantePerfil == ParticipantePerfilEnum.Administrador) &&
                                        w.Id == listaId && w.ListaStatus == ListaStatusEnum.Lixeira)
                                    .SingleOrDefaultAsync();
            if (lista == null)
                throw new ArgumentException("Exclusão não realizada!");

            context.RemoveRange(lista.Tarefas);
            context.RemoveRange(lista.ListaSecaos);
            context.RemoveRange(lista.Participantes);
            context.Remove(lista);
            await context.SaveChangesAsync();
        }
    }
}
