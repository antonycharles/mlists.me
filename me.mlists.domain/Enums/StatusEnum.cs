using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace me.mlists.domain.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "Ativo")]
        Ativo = 0,

        [Display(Name = "Inativo")]
        Inativo = 1
    }
}
