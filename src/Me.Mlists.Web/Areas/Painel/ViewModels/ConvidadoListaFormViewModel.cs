using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Painel.ViewModels
{
    public class ConvidadoListaFormViewModel
    {
        public IList<Convidado> Convidados { get; set; }

        public string ListaId { get; set; }

        [Display(Name = "E-mail convidado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-mail obrigatório!")]
        public string EmailConvidado{ get; set; }

        public Convidado ToConvidadoInsert(string userId)
        {
            return new Convidado(this.ListaId, this.EmailConvidado, userId, DateTime.Now);
        }
    }
}
