using Me.Mlists.Models;
using Me.Mlists.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.Controllers
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