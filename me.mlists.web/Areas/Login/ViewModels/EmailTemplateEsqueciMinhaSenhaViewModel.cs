using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace me.mlists.web.Areas.Login.ViewModels
{
    public class EmailTemplateEsqueciMinhaSenhaViewModel
    {
        public string Url { get; private set; }

        public EmailTemplateEsqueciMinhaSenhaViewModel(string url)
        {
            Url = url;
        }
    }
}
