using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using me.mlists.domain.Models;
using me.mlists.web.Areas.Login.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace me.mlists.web.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("l/registro-externo")]
    public class RegistroExternoController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistroExternoController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("chamada")]
        public IActionResult RegistroExterno(string provider)
        {
            var redirectUrl = Url.Action("RegistroExternoCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet("chamada-resultado")]
        public async Task<IActionResult> RegistroExternoCallbackAsync(string remoteError = null)
        {
            try
            {
                if (remoteError != null)
                    throw new ArgumentException($"Erro de provedor externo: {remoteError}");

                var info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                    throw new ArgumentException("Erro ao carregar informações de login externo");

                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (await _userManager.FindByEmailAsync(email) != null)
                    throw new ArgumentException("Este email já possui uma conta");

                var usuario = new ApplicationUser { Nome = info.Principal.FindFirstValue(ClaimTypes.Name), UserName = email, Email = email, EmailConfirmed = true };

                var resultado = await _userManager.CreateAsync(usuario);
                if (resultado.Succeeded)
                {
                    resultado = await _userManager.AddLoginAsync(usuario, info);
                    if (resultado.Succeeded)
                    {
                        await _signInManager.SignInAsync(usuario, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(Url.Action("Index", "Lista", new { area = "Painel" }));
                    }
                    else
                    {
                        throw new ArgumentException($"Não foi possível criar uma conta usando {info.ProviderDisplayName}");
                    }
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            var modelo = new RegistrarViewModel();
            modelo.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View("../Registro/Registrar", modelo);
        }
    }
}