using System;
using System.Linq;
using System.Threading.Tasks;
using Dev.Business.Core.Notifications;
using Dev.Business.Core.Services;
using Dev.Business.Models.Pontos.Validations;

namespace Dev.Business.Models.Pontos.Services
{
    public class PontoService : BaseService, IPontoService
    {
        private readonly IPontoRepository _pontoRepository;

        public PontoService(IPontoRepository pontoRepository, 
                            INotificador notificador) : base(notificador)
        {
            _pontoRepository = pontoRepository;
        }

        public async Task Adicionar(Ponto ponto)
        {
            if (!ExecutarValidacao(new PontoValidation(), ponto)) return;

            if (await ExistePontoAnterior(ponto)) return;

            await _pontoRepository.Adicionar(ponto);
        }

        public async Task Atualizar(Ponto ponto)
        {
            if (!ExecutarValidacao(new PontoValidation(), ponto)) return;

            if (await ExistePontoAnterior(ponto)) return;

            //if (await NomeFuncionarioAlterado(ponto.Id, ponto)) return;

            await _pontoRepository.Atualizar(ponto);
        }

        public async Task Remover(Guid id)
        {
            await _pontoRepository.Remover(id);
        }

        private async Task<bool> ExistePontoAnterior(Ponto ponto)
        {
            var pontoAtual = await _pontoRepository.Buscar(p => p.DataPonto >= ponto.DataPonto && p.Id != ponto.Id && p.FuncionarioId == ponto.FuncionarioId);

            if (pontoAtual.Any())
            {
                
                return true;
            }
                

            
            return false;
        }

        //private async Task<bool> NomeFuncionarioAlterado(Guid id, Ponto ponto)
        //{

        //    var pontoAtual = await _pontoRepository.ObterPorId(id);

        //    try
        //    {
        //        if (pontoAtual.Funcionario.Nome != ponto.Funcionario.Nome)
        //        {
        //            Notificar("O funcionário não pode ser alterado");
        //            return true;
        //        }
        //    }
        //    catch 
        //    (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
                      

        //    return false;

        //}

        public void Dispose()
        {
            _pontoRepository?.Dispose();
        }
    }
}
