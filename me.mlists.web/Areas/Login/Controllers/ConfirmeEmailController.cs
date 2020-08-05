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

        [HttpGet("",Name = "Confirmacao_email")]
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

        [HttpGet("reenviar-confirmacao-email", Name = "Reenviar_confirmacao_email")]
        public IActionResult ReenviarConfirmacaoEmail(string usuarioId)
        {
            if (usuarioId == null)
                return View("Error");

            ViewData["UsuarioId"] = usuarioId;

            return View();
        }
    }
}