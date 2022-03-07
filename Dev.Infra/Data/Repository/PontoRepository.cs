using Dev.Business.Models.Pontos;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev.Infra.Data.Context;

namespace Dev.Infra.Data.Repository
{
    public class PontoRepository : Repository<Ponto>, IPontoRepository
    {

        public PontoRepository(EasyPontoDbContext context) : base(context) { }

        public async Task<Ponto> ObterPontoFuncionario(Guid id)
        {
            return await Db.Pontos.AsNoTracking()
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Ponto>> ObterPontosFuncionarios()
        {
            return await Db.Pontos.AsNoTracking()
                .Include(f => f.Funcionario)
                .OrderBy(p => p.DataPonto).ToListAsync();
        }

        public async Task<IEnumerable<Ponto>> ObterPontosPorFuncionario(Guid funcionarioId)
        {
            return await Buscar(p => p.FuncionarioId == funcionarioId);
        }
    }
}
