using Me.Mlists.Models;
using Me.Mlists.Service.Services;
using Me.Mlists.Web.Areas.Login.ViewModels;
using Me.Mlists.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("l/esqueci-minha-senha")]
    public class EsqueciMinhaSenhaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;


        public EsqueciMinhaSenhaController(
             UserManager<ApplicationUser> userManager,
            IEmailService emailService
            )
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult EsqueciMinhaSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EsqueciMinhaSenhaAsync(EsqueciMinhaSenhaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(modelo.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToAction("EsqueciMinhaSenhaConfirmado");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                var callbackUrl = Url.RouteUrl("get_redefinir_senha", new { token },Request.Scheme);

                var templateEmail = await this.RenderViewToStringAsync("~/Areas/Login/Views/EsqueciMinhaSenha/partial/_EmailTemplateEsqueciMinhaSenha.cshtml",
                new EmailTemplateEsqueciMinhaSenhaViewModel(callbackUrl));

                await _emailService.SendEmailAsync(modelo.Email,"Alteração de senha - MLists", templateEmail);

                return RedirectToAction("EsqueciMinhaSenhaConfirmado");
            }

            return View(modelo);
        }

        [HttpGet("confirmacao")]
        public IActionResult EsqueciMinhaSenhaConfirmado()
        {
            return View();
        }
    }
}