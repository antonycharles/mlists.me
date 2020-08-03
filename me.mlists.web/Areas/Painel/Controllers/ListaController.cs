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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IListaRepository listaRepository;
        private readonly IMonsterRepository monsterRepository;

        public ListaController(
            UserManager<ApplicationUser> userManager,
            IListaRepository listaRepository,
            IMonsterRepository monsterRepository)
        {
            this.listaRepository = listaRepository;
            this.monsterRepository = monsterRepository;
            this.userManager = userManager;
        }

        [HttpGet("{isLixeira?}")]
        public async Task<IActionResult> IndexAsync(bool? isLixeira = false)
        {
            var listas = await listaRepository.GetAllListasByUserAsync(userManager.GetUserId(User), isLixeira);

            var listaViewModel = new ListaViewModel();
            listaViewModel.IsLixeira = isLixeira;
            listaViewModel.Listas = listas;

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
                    var lista = new Lista(userManager.GetUserId(User), modelo.Nome);
                    var listaResultado = await listaRepository.InsertListaAsync(lista);

                    var listaUrl = Url.Action("Index", "Tarefa", new { listaId = listaResultado.Id }, Request.Scheme);
                    return Json(new { isSucesso = true, listaUrl = HtmlEncoder.Default.Encode(listaUrl) });
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
            var lista = await listaRepository.GetListaByIdAsync(listaId, userManager.GetUserId(User));

            if(lista == null)
                return RedirectToAction("IndexAsync");

            var monsters = await monsterRepository.GetMonsters();

            var modelo = new ListaFormViewModel();
            modelo.Id = lista.Id;
            modelo.Nome = lista.Nome;
            modelo.Descricao = lista.Descricao;
            modelo.MonsterId = lista.MonsterId;
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
                    var lista = new Lista(modelo.Id,modelo.Nome,modelo.Descricao,modelo.MonsterId);
                    var resultado = await listaRepository.UpdateListaAsync(lista, userManager.GetUserId(User));


                    return Json(new { isSucesso = true, mensagem = "Alteração realizada com sucesso!", lista = resultado });
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
                if (ModelState.IsValid)
                {
                    await listaRepository.UpdateListaStatusLixeiraAsync(listaId, userManager.GetUserId(User));
                    return Json(new { isSucesso = true, mensagem = "Lista excluida com sucesso." });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return Json(new { isSucesso = false, mensagens = ModelState.Values.SelectMany(x => x.Errors) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("restaurar-ativo-lista/{listaId}", Name = "lista_post_restaurar_ativo")]
        public async Task<IActionResult> RestaurarListaStatusAtivoAsync(string listaId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await listaRepository.UpdateListaRestaurarStatusAtivoAsync(listaId, userManager.GetUserId(User));
                    return Json(new { isSucesso = true, mensagem = "Restauração realizada com sucesso!" });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return Json(new { isSucesso = false, mensagens = ModelState.Values.SelectMany(x => x.Errors) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("exclusao-permanente-lista/{listaId}", Name = "lista_post_exclusao_permanente")]
        public async Task<IActionResult> ExclusaoPermanenteListaAsync(string listaId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await listaRepository.ExcluirPermanenteListaAsync(listaId, userManager.GetUserId(User));
                    return Json(new { isSucesso = true, mensagem = "Lista excluida com sucesso." });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return Json(new { isSucesso = false, mensagens = ModelState.Values.SelectMany(x => x.Errors) });
        }
    }
}