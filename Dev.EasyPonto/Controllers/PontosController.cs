using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dev.EasyPonto.App_Start;
using Dev.EasyPonto.ViewModels;
using Dev.Business.Models.Pontos;
using Dev.Business.Models.Pontos.Services;
using AutoMapper;

namespace Dev.EasyPonto.Controllers
{
    public class PontosController : Controller
    {
        private readonly IPontoRepository _pontoRepository;
        private readonly IPontoService _pontoService;
        private readonly IMapper _mapper;

        public PontosController(IPontoRepository pontoRepository,
            IPontoService pontoService, IMapper mapper)
        {
            _pontoRepository = pontoRepository;
            _pontoService = pontoService;
            _mapper = mapper;

        }

        [Route ("lista-de-pontos")]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PontoViewModel>>(await _pontoRepository.ObterTodos()));
        }

        [Route ("dados-do-ponto/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var pontoViewModel = await ObterPontos(id);

            if (pontoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(pontoViewModel);
        }

        [Route("novo-ponto")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("novo-ponto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PontoViewModel pontoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _pontoService.Adicionar(_mapper.Map<Ponto>(pontoViewModel));
                
                return RedirectToAction("Index");
            }

            return View(pontoViewModel);
        }

        [Route("editar-ponto/{id:guid")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var pontoViewModel = await ObterPontos(id);

            if (pontoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(pontoViewModel);
        }

        [Route("editar-ponto/{id:guid")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PontoViewModel pontoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _pontoService.Atualizar(_mapper.Map<Ponto>(pontoViewModel));

                return RedirectToAction("Index");
            }
            return View(pontoViewModel);
        }

        [Route("excluir-ponto/{id:guid")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var pontoViewModel = await ObterPontos(id);

            if (pontoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(pontoViewModel);
        }

        [Route("excluir-ponto/{id:guid")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var pontoViewModel = await ObterPontos(id);

            if (pontoViewModel == null)
            {
                return HttpNotFound();
            }

            await _pontoService.Remover(id);
            
            return RedirectToAction("Index");
        }

        private async Task<PontoViewModel> ObterPontos(Guid id)
        {
            var ponto = _mapper.Map<PontoViewModel>(await _pontoRepository.ObterPontoFuncionario(id));
            
            return ponto;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pontoRepository?.Dispose();
                _pontoService?.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}
