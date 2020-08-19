using Me.Mlists.Models;
using Me.Mlists.Web.Areas.Login.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("l/login-externo")]
    public class LoginExternoController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginExternoController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("chamada")]
        public IActionResult LoginExterno(string provider, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Lista", new { area = "Painel" });

            var redirectUrl = Url.Action("LoginExternoCallback",new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet("chamada-resultado")]
        public async Task<IActionResult> LoginExternoCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            try
            {
                returnUrl = returnUrl ?? Url.Action("Index", "Lista", new { area = "Painel" });

                if (remoteError != null)
                    throw new ArgumentException($"Erro de provedor externo: {remoteError}");

                var info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                    throw new ArgumentException("Erro ao carregar informações de login externo");

                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else if (result.IsLockedOut)
                {
                    return RedirectToPage(Url.Action("Logoff", "Login", new { area = "Login" }));
                }
                else
                {
                    throw new ArgumentException($"Não foi possível fazer login usando {info.ProviderDisplayName}!");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }


            var modelo = new LoginViewModel();
            modelo.ReturnUrl = returnUrl;
            modelo.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View("../Login/Logar",modelo);
        }
    }
}