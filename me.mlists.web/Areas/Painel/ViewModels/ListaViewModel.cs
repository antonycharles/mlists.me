using me.mlists.domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace me.mlists.web.Areas.Painel.ViewModels
{
    public class ListaViewModel
    {
        public bool? IsLixeira { get; set; }

        public string UserId { get; set; }

        public IList<Lista> Listas { get; set; }

        public IList<ConvidadoRespostaFormViewModel> ConvitesViewModel { get; set; }

        public void setConvites(IList<Convidado> convites)
        {
            ConvitesViewModel = convites.Select(x => new ConvidadoRespostaFormViewModel(x.Id, this.UserId, x.Lista.Nome)).ToList();
        }
    }
}
