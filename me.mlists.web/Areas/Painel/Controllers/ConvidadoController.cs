using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using me.mlists.domain.Models;
using me.mlists.service.Repositories;
using me.mlists.service.Services;
using me.mlists.web.Areas.Painel.ViewModels;
using me.mlists.web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace me.mlists.web.Areas.Painel.Controllers
{
    [Authorize]
    [Area("Painel")]
    [Route("p/convidado")]
    public class ConvidadoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConvidadoRepository _convidadoRepository;
        private readonly IEmailService _emailService;

        public ConvidadoController(
            UserManager<ApplicationUser> userManager,
            IConvidadoRepository convidadoRepository,
            IEmailService emailService)
        {
            _convidadoRepository = convidadoRepository;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet("lista")]
        public async Task<IActionResult> ConvidadosLista(string listaId)
        {
            var convidados = await _convidadoRepository.GetAllConvidadosByListaIdAsync(listaId);
            var modelo = new ConvidadoListaFormViewModel();
            modelo.ListaId = listaId;
            modelo.Convidados = convidados;

            return PartialView("partial/_ConvidadosLista",modelo);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("insert-convidados")]
        public async Task<IActionResult> InsertConvidados([FromBody] ConvidadoListaFormViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var convidado = modelo.ToConvidadoInsert(_userManager.GetUserId(User));
                    var resultado = await _convidadoRepository.InsertConvidado(convidado);
                    this.EnviarEmailParaConvidadoAsync(resultado);
                    return Ok(new { mensagem = "Convite enviado com sucesso!" });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return BadRequest(new { mensagens = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("excluir-convite/{convidadoId}")]
        public async Task<IActionResult> ExcluirConviteAsync(string convidadoId)
        {
            try
            {
                await _convidadoRepository.ExcluirConvidado(convidadoId, _userManager.GetUserId(User));
                return Ok(new { mensagem = "Convite excluido com sucesso!" });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return BadRequest(new { mensagens = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("resposta-convite")]
        public async Task<IActionResult> RespostaContiteAsync([FromBody] ConvidadoRespostaFormViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var convidado = await _convidadoRepository.UpdateConvidadoStatusAsync(modelo.ToConvidadoUpdate());

                    if (modelo.IsAceitou)
                    {
                        await _convidadoRepository.ExcluirConvidado(convidado.Id, _userManager.GetUserId(User));
                        var listaUrl = Url.Action("Index", "Tarefa", new { listaId = convidado.ListaId }, Request.Scheme);
                        return Ok(new { url = HtmlEncoder.Default.Encode(listaUrl), mensagem = "Convite aceito!" });
                    }
                    else
                    {
                        return Ok(new { mensagem = "Convite não aceito com sucesso!" });
                    }
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return BadRequest(new { mensagens = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)) });
        }

        private async void EnviarEmailParaConvidadoAsync(Convidado convidado)
        {
            var callbackUrl = Url.ActionLink("Index", "Lista",
                new { area = "Painel" },
                Request.Scheme);

            var templateEmail = await this.RenderViewToStringAsync("~/Areas/Painel/Views/Convidado/partial/_EmailTemplateConvidado.cshtml",
                new EmailTemplateConvidadoViewModel(callbackUrl, User.Identity.Name));

            await _emailService.SendEmailAsync(convidado.EmailConvite, "Convite para Lista - MLists", templateEmail);
        }
    }
}