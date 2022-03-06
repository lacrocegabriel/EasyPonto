using System;
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

            await _pontoRepository.Adicionar(ponto);
        }

        public async Task Atualizar(Ponto ponto)
        {
            if (!ExecutarValidacao(new PontoValidation(), ponto)) return;

            await _pontoRepository.Atualizar(ponto);
        }

        public async Task Remover(Guid id)
        {
            await _pontoRepository.Remover(id);
        }

        public void Dispose()
        {
            _pontoRepository?.Dispose();
        }
    }
}
