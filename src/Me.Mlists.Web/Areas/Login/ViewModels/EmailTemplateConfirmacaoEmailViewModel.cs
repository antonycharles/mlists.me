using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.ViewModels
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
