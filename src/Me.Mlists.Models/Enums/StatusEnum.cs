using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Me.Mlists.Models.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "Ativo")]
        Ativo = 0,

        [Display(Name = "Inativo")]
        Inativo = 1
    }
}
