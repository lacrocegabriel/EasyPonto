using Dev.Business.Models.Funcionarios;
using Dev.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Infra.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {

        public EnderecoRepository(EasyPontoDbContext context) : base(context) { }
        public async Task<Endereco> ObterEnderecoPorFuncionario(Guid funcionarioId)
        {
            return await ObterPorId(funcionarioId);
        }
    }
}
