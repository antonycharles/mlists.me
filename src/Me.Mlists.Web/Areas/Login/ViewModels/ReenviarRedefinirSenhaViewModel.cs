using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.ViewModels
{
    public class EsqueciMinhaSenhaViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
