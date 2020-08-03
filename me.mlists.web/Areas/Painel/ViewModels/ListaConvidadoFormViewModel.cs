using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace me.mlists.web.Areas.Login.ViewModels
{
    public class ListaConvidadoFormViewModel
    {
        [Display(Name = "E-mail convidado")]
        public string EmailConvidado{ get; set; }
    }
}
