using System;
using System.Threading.Tasks;

namespace Dev.Business.Models.Funcionarios.Services
{
    public interface IFuncionarioService : IDisposable
    {
        Task Adicionar(Funcionario funcionario);
        Task Atualizar(Funcionario funcionario);
        Task Remover (Guid id);
        Task AtualizarEndereco(Endereco endereco);


    }
}
