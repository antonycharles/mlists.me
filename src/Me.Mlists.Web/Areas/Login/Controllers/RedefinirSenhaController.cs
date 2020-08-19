using Me.Mlists.Models;
using Me.Mlists.Web.Areas.Login.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Web.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("l/redefinir-senha")]
    public class RedefinirSenhaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RedefinirSenhaController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("", Name = "get_redefinir_senha")]
        public IActionResult RedefinirSenha(string token = null)
        {
            if (token == null)
            {
                return View("Error");
            }

            var modelo = new RedefinirSenhaViewModel();

            modelo.Token = token;

            return View(modelo);
        }

        [HttpPost("")]
        public async Task<IActionResult> RedefinirSenhaAsync(RedefinirSenhaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(modelo.Email);
                if (usuario == null)
                {
                    return RedirectToAction("RedefinirSenhaConfirmado");
                }

                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(modelo.Token));
                var resultado = await _userManager.ResetPasswordAsync(usuario, token, modelo.Senha);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("RedefinirSenhaConfirmado");
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(modelo);
        }

        [HttpGet("confirmacao")]
        public IActionResult RedefinirSenhaConfirmado()
        {
            return View();
        }
    }
}