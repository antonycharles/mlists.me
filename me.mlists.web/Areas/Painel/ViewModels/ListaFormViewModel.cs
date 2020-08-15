using me.mlists.domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace me.mlists.web.Areas.Painel.ViewModels
{
    public class ListaFormViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome lista obrigatório!")]
        [Display(Name = "Nome lista")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Monstros")]
        public int MonsterId { get; set; }
        public IList<Monster> Monsters { get; set; }

        public void setInformacoesLista(Lista lista)
        {
            this.Id = lista.Id;
            this.Nome = lista.Nome;
            this.Descricao = lista.Descricao;
            this.MonsterId = lista.MonsterId;
        }

        public Lista ToListaInsert(string userId)
        {
            return new Lista(userId, this.Nome);
        }

        public Lista ToListaUpdate()
        {
            return new Lista(this.Id, this.Nome, this.Descricao, this.MonsterId);
        }
    }
}
