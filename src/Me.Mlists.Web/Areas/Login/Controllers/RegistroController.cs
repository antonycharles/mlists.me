using Me.Mlists.Models;
using Me.Mlists.Service.Services;
using Me.Mlists.Web.Areas.Login.ViewModels;
using Me.Mlists.Web.Helpers;
using Me.Mlists.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.Controllers
{

    [Area("Login")]
    [Route("l/registro")]
    public class RegistroController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public RegistroController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarAsync()
        {
            var modelo = new RegistrarViewModel();
            modelo.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(modelo);
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

            var templateEmail = await this.RenderViewToStringAsync("~/Views/TemplateMail/_EmailComButton.cshtml", 
                new EmailComButtonViewModel(
                    titulo: "Seja bem vindo ao MLists!",
                    linkImagem: "https://img.icons8.com/bubbles/100/000000/meeting.png",
                    descricao: "Estamos felizes por ter você conosco. Para acessar sua conta primeiro você precisa confirmar seu email clicando no botão abaixo.",
                    link: callbackUrl,
                    linkDescricao: "Confirmar email")
                );

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