using me.mlists.domain.Models;
using System.Collections.Generic;

namespace me.mlists.web.Areas.Painel.ViewModels
{
    public class ListaViewModel
    {
        public bool? IsLixeira { get; set; }

        public IList<Lista> Listas { get; set; }
    }
}
