using System;
using System.Threading.Tasks;

namespace Dev.Business.Models.Pontos.Services
{
    public interface IPontoService : IDisposable 
    {
        Task Adicionar(Ponto ponto);
        Task Atualizar(Ponto ponto);
        Task Remover(Guid id);
    }
}
