using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using me.mlists.domain.Models;
using me.mlists.service.Repositories;
using me.mlists.service.Services;
using me.mlists.web.Areas.Painel.ViewModels;
using me.mlists.web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace me.mlists.web.Areas.Painel.Controllers
{
    [Authorize]
    [Area("Painel")]
    [Route("p/lista-tarefas")]
    public class TarefaController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IListaSecaoRepository listaSecaoRepository;
        private readonly IListaRepository listaRepository;
        private readonly ITarefaRepository tarefaRepository;

        public TarefaController(
            UserManager<ApplicationUser> userManager,
            IListaSecaoRepository listaSecaoRepository,
            ITarefaRepository tarefaRepository,
            IListaRepository listaRepository)
        {
            this.listaRepository = listaRepository;
            this.listaSecaoRepository = listaSecaoRepository;
            this.tarefaRepository = tarefaRepository;
            this.userManager = userManager;
        }

        [HttpGet("{listaId}")]
        public async Task<IActionResult> IndexAsync(string listaId)
        {
            var lista = await listaRepository.GetListaByIdAndAtivoAsync(listaId, userManager.GetUserId(User));

            if(lista == null)
                return RedirectToAction("Index","Lista", new { area = "Painel" });

            return View(lista);
        }

        [HttpGet("insert-tarefa/{listaId}")]
        public async Task<IActionResult> InsertTarefaAsync(string listaId)
        {
            var listaSecores = await listaSecaoRepository.GetListaSecoesByListaIdAsync(listaId);

            var tarefaFormViewModel = new TarefaFormViewModel();
            tarefaFormViewModel.ListaId = listaId;
            tarefaFormViewModel.ListaSecores = listaSecores;

            return PartialView("partial/_NovaTarefa", tarefaFormViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("insert-tarera-post/{listaId}")]
        public async Task<IActionResult> InsertTarefaPostAsync([FromBody] TarefaFormViewModel modelo, string listaId)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var tarefa = new Tarefa(modelo.Nome, modelo.ListaId, modelo.DataVencimento);
                    var tarefaResultado = await tarefaRepository.InsertTarefaAsync(tarefa, userManager.GetUserId(User));

                    var tarefaHtml = await this.RenderViewToStringAsync("~/Areas/Painel/Views/Tarefa/partial/_Tarefa.cshtml",tarefaResultado);

                    return Json(new { isSucesso = true, tarefaHtml = tarefaHtml });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return PartialView("partial/_NovaTarefa", modelo);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("marcar-tarefa-checked-true/{tarefaId}")]
        public async Task<IActionResult> MarcarTarefaCheckedTrueAsync(int tarefaId)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await tarefaRepository.UpdateTarefaCheckedTrueAsync(tarefaId, userManager.GetUserId(User));
                    return Json(new { isSucesso = true, mensagem = "Tarefa concluida com sucesso!" });
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return Json(new { isSucesso = false, mensagens = ModelState.Values.SelectMany(x => x.Errors) });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("mover-tarefa-lixeira/{tarefaId}")]
        public async Task<IActionResult> MoverTarefaLixeiraAsync(int tarefaId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await tarefaRepository.UpdateTarefaLixeiraTrueAsync(tarefaId, userManager.GetUserId(User));
                    return Json(new { isSucesso = true, mensagem = "Tarefa excluida com sucesso!" });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return Json(new { isSucesso = false, mensagens = ModelState.Values.SelectMany(x => x.Errors) });
        }
    }
}