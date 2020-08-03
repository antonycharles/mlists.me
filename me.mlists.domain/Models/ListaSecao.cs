using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.domain.Models
{
    public class ListaSecao
    {
        public int Id { get; private set; }
        public string ListaId { get; private set; }
        public Lista Lista { get; private set; }
        public string Descricao { get; private set; }
        public IList<Tarefa> Tarefas { get; private set; }
    }
}
