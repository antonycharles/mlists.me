using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace me.mlists.web.Areas.Login.ViewModels
{
    public class RegistrarViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome(apelido) obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email obrigatório!")]
        [EmailAddress(ErrorMessage = "Preencha com um email valido!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha obrigatório!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirmar senha obrigatório!")]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "A senha está diferente do valor confirmar senha.")]
        public string ConfirmarSenha { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
