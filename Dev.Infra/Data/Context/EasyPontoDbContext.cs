using Dev.Business.Models.Funcionarios;
using Dev.Business.Models.Pontos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Infra.Data.Context
{
    public class EasyPontoDbContext : DbContext
    {

        public EasyPontoDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Ponto> Pontos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

    }
}
