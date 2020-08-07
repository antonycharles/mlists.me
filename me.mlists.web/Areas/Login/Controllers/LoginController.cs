using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using me.mlists.domain.Models;
using me.mlists.web.Areas.Login.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace me.mlists.web.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("l/login")]
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public LoginController(
            SignInManager<ApplicationUser> signInManager,
             UserManager<ApplicationUser> userManager
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> LogarAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index","Lista",new { area = "Painel" });

            if (_signInManager.IsSignedIn(User))
            {
                return LocalRedirect(returnUrl);
            }

            var modelo = new LoginViewModel();
            modelo.ReturnUrl = returnUrl;
            modelo.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginViewModel modelo, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Lista", new { area = "Painel" });

            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(modelo.Email, modelo.Senha, modelo.LembrarDeMim, lockoutOnFailure: true);
                if (resultado.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else if (resultado.IsLockedOut && await IsCheckPasswordAsync(modelo))
                {
                    ModelState.AddModelError(string.Empty, "Usuario bloquedo por 5 minutos!");
                }
                else if (resultado.IsNotAllowed && await IsCheckPasswordAsync(modelo))
                {
                    var usuario = await _userManager.FindByEmailAsync(modelo.Email);
                    if(usuario != null)
                    {
                        return RedirectToRoute("Reenviar_confirmacao_email", new { usuarioId = usuario.Id });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou senha inválido!");
                }
            }

            return View(modelo);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logoff(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        private async Task<bool> IsCheckPasswordAsync(LoginViewModel modelo)
        {
            var user = await _userManager.FindByEmailAsync(modelo.Email);
            var checkPassword = await _userManager.CheckPasswordAsync(user, modelo.Senha);
            return checkPassword;
        }
    }
}