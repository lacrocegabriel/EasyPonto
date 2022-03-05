using Dev.Business.Core.Data;
using System;
using System.Threading.Tasks;

namespace Dev.Business.Models.Funcionarios
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Task<Funcionario> ObterFuncionarioPorEndereco(Guid id);
        Task<Funcionario> ObterFuncionarioPontosEndereco(Guid id);

    }
}
