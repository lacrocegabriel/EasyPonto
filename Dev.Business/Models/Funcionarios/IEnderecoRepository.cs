using Dev.Business.Core.Data;
using System;
using System.Threading.Tasks;

namespace Dev.Business.Models.Funcionarios
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFuncionario(Guid funcionarioId);

    }
}
