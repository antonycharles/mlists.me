using Me.Mlists.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Painel.ViewModels
{
    public class CardListaDropDownViewModel
    {
        public CardListaDropDownViewModel(Lista lista, bool isAtiva, bool isAdministrador, bool isVisualizar = true)
        {
            Lista = lista;
            IsAtiva = isAtiva;
            IsAdministrador = isAdministrador;
            IsBtnVisualizar = isVisualizar;
        }

        public Lista Lista { get; set; }

        public bool IsAtiva { get; set; }
        public bool IsAdministrador { get; set; }

        public bool IsBtnVisualizar { get; set; }
    }
}
