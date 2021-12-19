using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev.Business.Models.Funcionarios;
using System.Data.Entity;

namespace Dev.Infra.Data.Repository
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public async Task<Funcionario> ObterFucionarioPontosEndereco(Guid id)
        {
            return await Db.Funcionarios.AsNoTracking()
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Funcionario> ObterFuncionarioPorEndereco(Guid id)
        {
            return await Db.Funcionarios.AsNoTracking()
                .Include(f => f.Endereco)
                .Include(f => f.Pontos)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        //public Funcionario Test()
        //{
        //    return Buscar(f => f.Ativo && f.Documento == "" && f.Nome == "")
        //        .Result.FirstOrDefault();
        //}

        public override async Task Remover(Guid id)
        {
            var funcionario = await ObterPorId(id);
            funcionario.Ativo = false;

            await Atualizar(funcionario);
        }

    }
}
