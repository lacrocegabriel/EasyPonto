using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Dev.Business.Core.Notifications;
using Dev.Business.Models.Funcionarios;
using Dev.Business.Models.Funcionarios.Services;
using Dev.EasyPonto.ViewModels;
using static Dev.EasyPonto.Extensions.CustomAuthorization;

namespace Dev.EasyPonto.Controllers
{
    
    public class FuncionariosController : BaseController
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;


        public FuncionariosController(IFuncionarioRepository funcionarioRepository, 
                                      IMapper mapper, 
                                      IFuncionarioService funcionarioService,
                                      INotificador notificador) : base(notificador)
        {
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
            _funcionarioService = funcionarioService;
        }

        [Route("lista-de-funcionarios")]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FuncionarioViewModel>>(await _funcionarioRepository.ObterTodos()));
        }

        [Route("dados-do-funcionario/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var funcionarioViewModel = await ObterFuncionarioEndereco(id);

            if(funcionarioViewModel == null)
            {
                return HttpNotFound();
            }
            return View(funcionarioViewModel);
        }

        
        [Route("novo-funcionario")]
        public ActionResult Create()
        {
            return View();
        }

        
        [Route ("novo-funcionario")]
        [HttpPost]
        public async Task<ActionResult> Create (FuncionarioViewModel funcionarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(funcionarioViewModel);
            }

            var funcionario = _mapper.Map<Funcionario>(funcionarioViewModel);
            await _funcionarioService.Adicionar(funcionario);

            if (!OperacaoValida()) return View(funcionarioViewModel);
            
            ViewBag.Sucesso = "Funcionario Cadastrado";

            return RedirectToAction("Index");
        }



        [Route("editar-funcionario/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var funcionarioViewModel = await ObterFuncionarioPontosEndereco(id);

            if(funcionarioViewModel == null)
            {
                return HttpNotFound();
            }

            return View(funcionarioViewModel);
        }


        [Route("editar-funcionario/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, FuncionarioViewModel funcionarioViewModel)
        {
            if (id != funcionarioViewModel.Id) return HttpNotFound();

            if (!ModelState.IsValid) return View(funcionarioViewModel);

            var funcionario = _mapper.Map<Funcionario>(funcionarioViewModel);
            await _funcionarioService.Atualizar(funcionario);

            return RedirectToAction("Index");

        }


        [Route("excluir-funcionario/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var funcionarioViewModel = await ObterFuncionarioPontosEndereco(id);

            if (funcionarioViewModel == null)
            {
                return HttpNotFound();
            }

            return View(funcionarioViewModel);
        }

        [Route("excluir-funcionario/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var funcionario = await ObterFuncionarioEndereco(id);

            if (funcionario == null)
            {
                return HttpNotFound();
            }

            await _funcionarioService.Remover(id);

            return View("Index");
        }

        [Route ("obter-endereco-funcionario/{id:guid}")]
        public async Task<ActionResult> ObterEndereco(Guid id)
        {
            var funcionario = await ObterFuncionarioEndereco(id);

            if (funcionario == null)
            {
                return HttpNotFound();
            }

            return PartialView("_DetalhesEndereco", funcionario);
        }
        
        [Route ("atualizar-endereco-funcionario/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> AtualizarEndereco(Guid id)
        {
            var funcionario = await ObterFuncionarioEndereco(id);

            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AtualizarEndereco", new FuncionarioViewModel { Endereco = funcionario.Endereco });
        }

        [Route("atualizar-endereco-funcionario/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> AtualizarEndereco(FuncionarioViewModel funcionarioViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", funcionarioViewModel);

            await _funcionarioService.AtualizarEndereco(_mapper.Map<Endereco>(funcionarioViewModel.Endereco)); ;

            var url = Url.Action("ObterEndereco", "Funcionarios", new { id = funcionarioViewModel.Endereco.FuncionarioId });

            return Json(new { success = true, url });
        }

        

        private async Task<FuncionarioViewModel> ObterFuncionarioEndereco (Guid id)
        {
            return _mapper.Map<FuncionarioViewModel>(await _funcionarioRepository.ObterFuncionarioPorEndereco(id));
        }

        private async Task<FuncionarioViewModel> ObterFuncionarioPontosEndereco(Guid id)
        {
            return _mapper.Map<FuncionarioViewModel>(await _funcionarioRepository.ObterFuncionarioPontosEndereco(id));
        }
    }
}