using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Repositories
{
    public interface ITarefaRepository
    {
        Task<Tarefa> InsertTarefaAsync(Tarefa tarefa, string userId);

        Task<Tarefa> UpdateTarefaAsync(Tarefa tarefa, string userId);

        Task UpdateTarefaCheckedTrueAsync(int tarefaId, string userId);

        Task UpdateTarefaLixeiraTrueAsync(int tarefaId, string userId);
    }
}
