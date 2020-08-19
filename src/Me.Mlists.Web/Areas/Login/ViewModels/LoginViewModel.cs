using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email obrigatório!")]
        [EmailAddress(ErrorMessage = "Preencha com um email valido!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Manter conectado")]
        public bool LembrarDeMim { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
