using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.ViewModels
{
    public class RedefinirSenhaViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha obrigatório!")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirmar senha obrigatório!")]
        [Display(Name = "Confirmar nova senha")]
        [Compare("Senha", ErrorMessage = "A senha está diferente do valor confirmar senha.")]
        public string ConfirmarSenha { get; set; }

        public string Token { get; set; }
    }
}
