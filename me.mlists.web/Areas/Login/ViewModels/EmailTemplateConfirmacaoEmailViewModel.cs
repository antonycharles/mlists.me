using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace me.mlists.web.Areas.Login.ViewModels
{
    public class EmailTemplateConfirmacaoEmailViewModel
    {
        public string Url { get; private set; }

        public EmailTemplateConfirmacaoEmailViewModel(string url)
        {
            Url = url;
        }
    }
}
