using me.mlists.domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace me.mlists.service.Repositories
{
    public interface ITarefaRepository
    {
        Task<Tarefa> InsertTarefaAsync(Tarefa tarefa, string userId);

        Task<Tarefa> UpdateTarefaAsync(Tarefa tarefa, string userId);

        Task UpdateTarefaCheckedTrueAsync(int tarefaId, string userId);

        Task UpdateTarefaLixeiraTrueAsync(int tarefaId, string userId);
    }
}
