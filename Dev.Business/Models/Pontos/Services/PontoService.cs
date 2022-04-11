using System;
using System.Linq;
using System.Threading.Tasks;
using Dev.Business.Core.Notifications;
using Dev.Business.Core.Services;
using Dev.Business.Models.Funcionarios;
using Dev.Business.Models.Pontos.Validations;

namespace Dev.Business.Models.Pontos.Services
{
    public class PontoService : BaseService, IPontoService
    {
        private readonly IPontoRepository _pontoRepository;
        
        public PontoService(IPontoRepository pontoRepository,
                            IFuncionarioRepository funcionarioRepository,
                            INotificador notificador) : base(notificador)
        {
            _pontoRepository = pontoRepository;
        }

        public async Task Adicionar(Ponto ponto)
        {
            if (!ExecutarValidacao(new PontoValidation(), ponto)) return;

            await _pontoRepository.Adicionar(ponto);
        }

        public async Task Atualizar(Ponto ponto)
        {
            if (!ExecutarValidacao(new PontoValidation(), ponto)) return;

            if (await ExistePontoAnterior(ponto)) return;

            await _pontoRepository.Atualizar(ponto);

        }

        public async Task Remover(Guid id)
        {
            await _pontoRepository.Remover(id);
        }

        private async Task<bool> ExistePontoAnterior(Ponto ponto)
        {
            var pontoAtual = await _pontoRepository.ObterPorId(ponto.Id);


            if (ponto.DataPonto >= pontoAtual.DataPonto)
            {
                var pontos = await _pontoRepository.Buscar(p => p.DataPonto >= ponto.DataPonto && p.Id != ponto.Id && p.FuncionarioId == ponto.FuncionarioId);

                if (pontos.Any())
                {
                    Notificar("Operação não permitida! Há um ou mais pontos com data superior a informada!");
                    return true;
                }
                
            }
            else
            {
                var pontos = await _pontoRepository.Buscar(p => p.DataPonto <= pontoAtual.DataPonto && p.Id != ponto.Id && p.FuncionarioId == ponto.FuncionarioId);
                
                if (pontos.Where(p => p.DataPonto > ponto.DataPonto).Any())
                {
                    Notificar("Operação não permitida! Há um ou mais pontos com data inferior a informada!");
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            _pontoRepository?.Dispose();
        }
    }
}
