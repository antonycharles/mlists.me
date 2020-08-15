using me.mlists.domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace me.mlists.web.Areas.Painel.ViewModels
{
    public class TarefaFormViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tarefa obrigatório!")]
        [Display(Name = "Tarefa")]
        public string Nome { get; set; }

        [Display(Name = "Data vencimento")]
        public DateTime? DataVencimento { get; set; }

        public string ListaId { get; set; }

        public int? ListaSecaoId { get; set; }

        public IList<ListaSecao> ListaSecores { get; set; }

        public Tarefa ToTarefaInsert()
        {
            return new Tarefa(this.Nome, this.ListaId, this.DataVencimento);
        }
    }
}
