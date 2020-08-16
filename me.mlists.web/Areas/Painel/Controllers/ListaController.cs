using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using me.mlists.domain.Enums;
using me.mlists.domain.Models;
using me.mlists.service.Repositories;
using me.mlists.web.Areas.Painel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace me.mlists.web.Areas.Painel.Controllers
{
    [Authorize]
    [Area("Painel")]
    [Route("p/lista")]
    public class ListaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IListaRepository _listaRepository;
        private readonly IConvidadoRepository _convidadoRepository;
        private readonly IMonsterRepository _monsterRepository;

        public ListaController(
            UserManager<ApplicationUser> userManager,
            IListaRepository listaRepository,
            IConvidadoRepository convidadoRepository,
            IMonsterRepository monsterRepository)
        {
            _listaRepository = listaRepository;
            _monsterRepository = monsterRepository;
            _convidadoRepository = convidadoRepository;
            _userManager = userManager;
        }

        [HttpGet("{isLixeira?}")]
        public async Task<IActionResult> IndexAsync(bool? isLixeira = false)
        {
            var listas = await _listaRepository.GetAllListasByUserAsync(_userManager.GetUserId(User), isLixeira);
            var convites = await _convidadoRepository.GetAllConvidadosByEmailAsync(User.Identity.Name);

            var listaViewModel = new ListaViewModel();
            listaViewModel.IsLixeira = isLixeira;
            listaViewModel.Listas = listas;
            listaViewModel.UserId = _userManager.GetUserId(User);
            listaViewModel.setConvites(convites);

            return View(listaViewModel);
        }

        [HttpGet("insert-lista")]
        public IActionResult InsertLista()
        {
            return PartialView("partial/_NovaLista");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("insert-lista", Name = "lista_post_insert_lista")]
        public async Task<IActionResult> InsertListaAsync([FromBody] ListaFormViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var lista = modelo.ToListaInsert(_userManager.GetUserId(User));
                    var listaResultado = await _listaRepository.InsertListaAsync(lista);

                    var listaUrl = Url.Action("Index", "Tarefa", new { listaId = listaResultado.Id }, Request.Scheme);
                    return Ok(new { listaUrl = HtmlEncoder.Default.Encode(listaUrl) });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return PartialView("partial/_NovaLista", modelo);
        }

        [HttpGet("update-lista/{listaId}", Name = "lista_get_update_lista")]
        public async Task<IActionResult> UpdateListaAsync(string listaId)
        {
            var lista = await _listaRepository.GetListaByIdAsync(listaId, _userManager.GetUserId(User));

            if(lista == null)
                return RedirectToAction("IndexAsync");

            var monsters = await _monsterRepository.GetMonsters();

            var modelo = new ListaFormViewModel();
            modelo.setInformacoesLista(lista);
            modelo.Monsters = monsters;

            return PartialView("partial/_UpdateLista",modelo);
        }

        [HttpPost("update-lista/{listaId}", Name = "lista_post_update_lista")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateListaAsync([FromBody] ListaFormViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var lista = modelo.ToListaUpdate();
                    var resultado = await _listaRepository.UpdateListaAsync(lista, _userManager.GetUserId(User));

                    return Ok(new { mensagem = "Alteração realizada com sucesso!", lista = resultado });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return PartialView("partial/_UpdateLista", modelo);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("mover-lista-lixeira/{listaId}", Name = "lista_post_mover_lixeira_lista")]
        public async Task<IActionResult> MoverListaLixeiraAsync(string listaId)
        {
            try
            {
                await _listaRepository.UpdateListaStatusLixeiraAsync(listaId, _userManager.GetUserId(User));
                return Ok(new { mensagem = "Lista excluida com sucesso." });
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return BadRequest(new { mensagens = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("restaurar-ativo-lista/{listaId}", Name = "lista_post_restaurar_ativo")]
        public async Task<IActionResult> RestaurarListaStatusAtivoAsync(string listaId)
        {
            try
            {
                await _listaRepository.UpdateListaRestaurarStatusAtivoAsync(listaId, _userManager.GetUserId(User));
                return Ok(new { mensagem = "Restauração realizada com sucesso!" });
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return BadRequest(new { mensagens = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("exclusao-permanente-lista/{listaId}", Name = "lista_post_exclusao_permanente")]
        public async Task<IActionResult> ExclusaoPermanenteListaAsync(string listaId)
        {
            try
            {
                await _listaRepository.ExcluirPermanenteListaAsync(listaId, _userManager.GetUserId(User));
                return Ok(new { mensagem = "Lista excluida com sucesso." });
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return BadRequest(new { mensagens = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)) });
        }
    }
}