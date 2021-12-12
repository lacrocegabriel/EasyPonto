using Dev.Business.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dev.Business.Models.Pontos
{
    public interface IPontoRepository :  IRepository<Ponto>
    {
        Task<IEnumerable<Ponto>> ObterPontosPorFuncionario(Guid funcionarioId);
        Task<IEnumerable<Ponto>> ObterPontosFuncionarios();
        Task<Ponto> ObterPontoFuncionario(Guid id);
    }
}
