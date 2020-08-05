using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using me.mlists.domain.Models;
using me.mlists.service.Services;
using me.mlists.web.Areas.Login.ViewModels;
using me.mlists.web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace me.mlists.web.Areas.Login.Controllers
{

    [Area("Login")]
    [Route("l/registro")]
    public class RegistroController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public RegistroController(
            UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService; 
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(modelo.Email) != null)
                    return View("../ConfirmeEmail/AguardandoConfirmacao");

                var usuario = new ApplicationUser { Nome = modelo.Nome, UserName = modelo.Email, Email = modelo.Email };
                var resultado = await _userManager.CreateAsync(usuario, modelo.Senha);

                if (resultado.Succeeded)
                {

                    await EnviarEmailDeConfirmacaoAsync(usuario);
                    return View("../ConfirmeEmail/AguardandoConfirmacao");
                }
                else
                {
                    AdicionarErros(resultado);
                }
               
            }

            return View(modelo);
        }

        [HttpPost("reenviar-confirmacao-email", Name = "form_reenviar_confirmacao_email")]
        public async Task<IActionResult> ReenviarConfirmacaoEmailAsync(string usuarioId = null)
        {
            if (usuarioId == null)
                return View("Error");

            var usuario = await _userManager.FindByIdAsync(usuarioId);
            await EnviarEmailDeConfirmacaoAsync(usuario);

            return View("../ConfirmeEmail/AguardandoConfirmacao");
        }

        private async Task EnviarEmailDeConfirmacaoAsync(ApplicationUser usuario)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = Url.RouteUrl("Confirmacao_email",
                new { usuarioId = usuario.Id, token = token },
                Request.Scheme);

            var templateEmail = await this.RenderViewToStringAsync("~/Areas/Login/Views/ConfirmeEmail/partial/_EmailTemplateConfirmacaoEmail.cshtml", 
                new EmailTemplateConfirmacaoEmailViewModel(callbackUrl));

            await _emailService.SendEmailAsync(usuario.Email, "Confirmação de email - MLists", templateEmail);

        }

        private void AdicionarErros(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}