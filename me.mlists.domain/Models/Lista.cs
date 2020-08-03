using me.mlists.domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.domain.Models
{
    public class Lista
    {
        public string Id { get; private set; }
        public string CriadoPorId { get; private set; } 
        public ApplicationUser CriadoPor { get; private set; }
        public int MonsterId { get; private set; }
        public Monster Monster { get; private set; }
        public ListaStatusEnum ListaStatus { get; private set; }
        public IList<ListaSecao> ListaSecaos { get; private set; }
        public IList<Tarefa> Tarefas { get; private set; }
        public IList<Participante> Participantes { get; private set; }
        public IList<Convidado> Convidados { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public Lista(string criadoPorId, string nome)
        {
            CriadoPorId = criadoPorId;
            ListaStatus = ListaStatusEnum.Ativo;
            Nome = nome;
        }

        public Lista(string id, string nome, string descricao, int monsterId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            MonsterId = monsterId;
        }

        public void setStatusEnumLixeira()
        {
            if(this.ListaStatus == ListaStatusEnum.Ativo)
                this.ListaStatus = ListaStatusEnum.Lixeira;
        }

        public void setStatusEnumAtivo()
        {
            if (this.ListaStatus == ListaStatusEnum.Lixeira)
                this.ListaStatus = ListaStatusEnum.Ativo;
        }

        public void setMonster(Monster monster)
        {
            Monster = monster;
            MonsterId = monster.Id;
        }

        public void Update(Lista lista)
        {
            this.Nome = lista.Nome;
            this.Descricao = lista.Descricao;
            this.MonsterId = lista.MonsterId;
        }
    }
}
