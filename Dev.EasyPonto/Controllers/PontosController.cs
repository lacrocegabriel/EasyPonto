using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dev.EasyPonto.ViewModels;
using Dev.Business.Models.Pontos;
using Dev.Business.Models.Pontos.Services;
using AutoMapper;
using Dev.Business.Models.Funcionarios;
using Dev.Business.Core.Notifications;

namespace Dev.EasyPonto.Controllers
{
    public class PontosController : BaseController
    {
        private readonly IPontoRepository _pontoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IPontoService _pontoService;
        private readonly IMapper _mapper;

        public PontosController(IPontoRepository pontoRepository,
            IPontoService pontoService, 
            IFuncionarioRepository funcionarioRepository,
            IMapper mapper,
            INotificador notificador) : base (notificador)
        {
            _pontoRepository = pontoRepository;
            _pontoService = pontoService;
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
        }

        [Route ("lista-de-pontos")]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PontoViewModel>>(await _pontoRepository.ObterPontosFuncionarios()));
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
        public async Task<ActionResult> Create()
        {
            var pontoViewModel = await PopularFuncionarios(new PontoViewModel());

            return View(pontoViewModel);
        }

        [Route("novo-ponto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PontoViewModel pontoViewModel)
        {
            pontoViewModel = await PopularFuncionarios(pontoViewModel);

            pontoViewModel.DataPonto = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _pontoService.Adicionar(_mapper.Map<Ponto>(pontoViewModel));

                if (!OperacaoValida()) return View(pontoViewModel);
            }

            return RedirectToAction("Index");
        }

        [Route("editar-ponto/{id:guid}")]
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

        [Route("editar-ponto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PontoViewModel pontoViewModel)
        {
            pontoViewModel = await PopularFuncionarios(pontoViewModel);
            pontoViewModel = await PopularFuncionarioId(pontoViewModel);
                       

            if (ModelState.IsValid)
            {
                await _pontoService.Atualizar(_mapper.Map<Ponto>(pontoViewModel));

                if (!OperacaoValida()) return View(pontoViewModel);

            }
            
            return RedirectToAction("Index");
        }

        [Route("excluir-ponto/{id:guid}")]
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

        [Route("excluir-ponto/{id:guid}")]
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
            ponto.Funcionarios = _mapper.Map<IEnumerable<FuncionarioViewModel>>(await _funcionarioRepository.ObterTodos());
            return ponto;
        }

        private async Task<PontoViewModel> PopularFuncionarios(PontoViewModel ponto)
        {
            ponto.Funcionarios = _mapper.Map<IEnumerable<FuncionarioViewModel>>(await _funcionarioRepository.ObterTodos());

            return ponto;
        }

        private async Task<PontoViewModel> PopularFuncionarioId(PontoViewModel ponto)
        {
            var pontoAtual = _mapper.Map<PontoViewModel>(await _pontoRepository.ObterPontoFuncionario(ponto.Id));


            ponto.FuncionarioId = pontoAtual.FuncionarioId;

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
