using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace me.mlists.domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }

    }
}
