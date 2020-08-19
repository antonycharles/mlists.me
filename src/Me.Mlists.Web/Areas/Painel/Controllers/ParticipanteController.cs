using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Me.Mlists.Models;
using Me.Mlists.Service.Repositories;
using Me.Mlists.Web.Areas.Painel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Me.Mlists.Web.Areas.Painel.Controllers
{
    [Authorize]
    [Area("Painel")]
    [Route("p/participante")]
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParticipanteController(IParticipanteRepository participanteRepository,
            UserManager<ApplicationUser> userManager)
        {
            _participanteRepository = participanteRepository;
            _userManager = userManager;
        }

        [HttpGet("lista/{listaId}")]
        public async Task<IActionResult> ParticipantesListaAsync(string listaId)
        {
            var participantes = await _participanteRepository.GetParticipanteAllByIdAndListaId(listaId, _userManager.GetUserId(User));
            
            foreach(var item in participantes)
            {
                item.User = await _userManager.FindByIdAsync(item.UserId);
            }

            var modelo = new ParticipanteFormViewModel();
            modelo.participantes = participantes;

            return PartialView("partial/_ParticipantesLista", modelo);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("update-participante")]
        public async Task<IActionResult> UpdateParticipanteAsync([FromBody] ParticipanteFormViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var participante = modelo.ToParticipanteUpdate();
                    var resultado = await _participanteRepository.UpdatePerfilParticipanteAsync(participante, _userManager.GetUserId(User));

                    return Ok(new { mensagem = "Alteração realizada com sucesso!" });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            
            return BadRequest(new { mensagens = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)) });
        }
    }
}