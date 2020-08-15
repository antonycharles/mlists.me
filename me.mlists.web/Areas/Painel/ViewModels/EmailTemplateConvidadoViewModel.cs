using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace me.mlists.web.Areas.Painel.ViewModels
{
    public class EmailTemplateConvidadoViewModel
    {
        public string Url { get; private set; }
        public string Nome { get; private set; }

        public EmailTemplateConvidadoViewModel(string url, string nome)
        {
            Url = url;
            Nome = nome;
        }
    }
}
