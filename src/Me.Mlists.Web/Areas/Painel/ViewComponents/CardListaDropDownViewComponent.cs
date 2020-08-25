using Me.Mlists.Models;
using Me.Mlists.Models.Enums;
using Me.Mlists.Web.Areas.Painel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Painel.ViewComponents
{
    public class CardListaDropDownViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CardListaDropDownViewComponent(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(Lista lista, bool isVisualizar = true)
        {
            var isAtiva = lista.ListaStatus == ListaStatusEnum.Ativo;
            var isAdministrador = lista.Participantes
                    .Where(w =>
                        w.UserId == _userManager.GetUserId(UserClaimsPrincipal) &&
                        w.ParticipantePerfil == ParticipantePerfilEnum.Administrador
                    ).Count() > 0;

            return View("~/Areas/Painel/Views/Lista/Components/CardListaDropDown/Default.cshtml", new CardListaDropDownViewModel(lista,isAtiva,isAdministrador,isVisualizar));
        }
    }
}
