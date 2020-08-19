using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Painel.ViewModels
{
    public class ConvidadoRespostaFormViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Convidado id obrigatório!")]
        public string ConvidadoId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User id obrigatório!")]
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Resposta obrigatório!")]
        public bool IsAceitou { get; set; }

        public string ListaNome { get; set; }

        public ConvidadoRespostaFormViewModel(string convidadoId, string userId, string listaNome)
        {
            ConvidadoId = convidadoId;
            UserId = userId;
            ListaNome = listaNome;
        }

        public Convidado ToConvidadoUpdate()
        {
            return new Convidado(this.ConvidadoId, this.UserId, this.IsAceitou);
        }
    }
}
