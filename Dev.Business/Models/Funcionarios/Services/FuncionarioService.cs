using Dev.Business.Core.Notifications;
using Dev.Business.Core.Services;
using Dev.Business.Models.Funcionarios.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Business.Models.Funcionarios.Services
{
    public class FuncionarioService : BaseService, IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                                  IEnderecoRepository enderecoRepository,
                                  INotificador notificador) : base(notificador)
        {
            _funcionarioRepository = funcionarioRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Funcionario funcionario)
        {
            if (!ExecutarValidacao(new FuncionarioValidation(), funcionario)
                || !ExecutarValidacao(new EnderecoValidation(), funcionario.Endereco)) return;

            if (await FuncionarioExistente(funcionario)) return;

            await _funcionarioRepository.Adicionar(funcionario);
        }

        public async Task Atualizar(Funcionario funcionario)
        {
            if (!ExecutarValidacao(new FuncionarioValidation(), funcionario)) return;

            if (await FuncionarioExistente(funcionario)) return;

            await _funcionarioRepository.Atualizar(funcionario);
        }

        public async Task Remover(Guid id)
        {
            var funcionario = await _funcionarioRepository.ObterFuncionarioPontosEndereco(id);

            if (funcionario.Pontos.Any())
            {
                Notificar("O funcionario possui pontos cadastrados");
                return;
            }

            if(funcionario.Endereco != null)
            {
                await _enderecoRepository.Remover(funcionario.Id);
            }

            await _funcionarioRepository.Remover(id);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        private async Task<bool> FuncionarioExistente(Funcionario funcionario)
        {
            var funcionarioAtual = await _funcionarioRepository.Buscar(f => f.Documento == funcionario.Documento
            && f.Id != funcionario.Id);

            if (!funcionarioAtual.Any()) return false;

            Notificar("Já existe um funcionario com este documento informado");
            return true;
        }

        public void Dispose()
        {
            _funcionarioRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }

       
    }
}
