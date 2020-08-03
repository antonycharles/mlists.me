using me.mlists.domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.domain.Models
{
    public class Tarefa
    {
        public int Id { get; private set; }
        public string ListaId { get; private set; }
        public Lista Lista { get; private set; }
        public int ParticipanteId { get; private set; }
        public Participante Participante { get; private set; }
        public int? ListaSecaoId { get; private set; }
        public ListaSecao ListaSecao { get; private set; }
        public string Nome { get; private set; }
        public DateTime? DataVencimento { get; private set; }
        public bool IsChecked { get; private set; }
        public bool IsLixeira { get; private set; }

        public Tarefa(string nome, string listaId, DateTime? dataVencimento)
        {
            Nome = nome;
            ListaId = listaId;

            if(dataVencimento != null)
            {
                setDataVencimento((DateTime)dataVencimento);
            }
        }

        private void setDataVencimento(DateTime dataVencimento)
        {
            if(dataVencimento <= DateTime.Now)
            {
                throw new ArgumentException("Data de vencimento não pode ser inferior a data atual!");
            }

            DataVencimento = dataVencimento;
        }

        public void setListaSecao(ListaSecao listaSecao)
        {
            ListaSecao = listaSecao;

            if (listaSecao.Id != 0)
            {
                ListaSecaoId = listaSecao.Id;
            }
        }

        public void setParticipante(Participante participante)
        {
            if(participante.Id == 0 || participante.ParticipantePerfil == ParticipantePerfilEnum.Bloqueado)
            {
                throw new ArgumentException("Participante não pode ser incluido na tarefa!");
            }

            Participante = participante;
            ParticipanteId = participante.Id;
        }

        public void Update(Tarefa tarefa)
        {
            this.Nome = tarefa.Nome;
            this.DataVencimento = tarefa.DataVencimento;
            this.ListaSecaoId = tarefa.ListaSecaoId;
        }


        public void setTarefaCheckedTrue()
        {
            IsChecked = true;
            this.setLixeiraTrue();
        }

        public void setLixeiraTrue()
        {
            IsLixeira = true;
        }
    }
}
