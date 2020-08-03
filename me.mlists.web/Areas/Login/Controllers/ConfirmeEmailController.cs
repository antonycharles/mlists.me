using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using me.mlists.domain.Models;
using me.mlists.service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace me.mlists.web.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("l/confirmacao-email")]
    public class ConfirmeEmailController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public ConfirmeEmailController(
            UserManager<ApplicationUser> userMan,
            IEmailService emailService)
        {
            _userManager = userMan;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("",Name = "Confirmacao_email")]
        public async Task<IActionResult> ConfirmacaoEmailAsync(string usuarioId, string token)
        {
            if (usuarioId == null || token == null)
                return View("Error");

            var usuario = await _userManager.FindByIdAsync(usuarioId);

            if (usuario == null)
                return View("Error");

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var resultado = await _userManager.ConfirmEmailAsync(usuario, token);

            if (resultado.Succeeded)
            {
                return View("ConfirmacaoEmailAsync");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("reenviar-confirmacao-email", Name = "Reenviar_confirmacao_email")]
        public async Task<IActionResult> ReenviarConfirmacaoEmailAsync(string usuarioId)
        {
            if (usuarioId == null)
                return View("Error");

            ViewData["UsuarioId"] = usuarioId;

            return View();
        }

        [HttpPost]
        [Route("reenviar-novamente-confirmacao-email", Name = "Reenviar_novamente_confirmacao_email")]
        public async Task<IActionResult> ReenviarNovamenteConfirmacaoEmailAsync(string usuarioId = null)
        {
            if (usuarioId == null)
                return View("Error");

            var usuario = await _userManager.FindByIdAsync(usuarioId);
            await EnviarEmailDeConfirmacaoAsync(usuario);

            return View("AguardandoConfirmacao");
        }


        private async Task EnviarEmailDeConfirmacaoAsync(ApplicationUser usuario)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = Url.RouteUrl("Confirmacao_email",
                new { usuarioId = usuario.Id, token = token },
                Request.Scheme);

            await _emailService.SendEmailAsync(usuario.Email, "Confirme seu email - MLists",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        }
    }
}