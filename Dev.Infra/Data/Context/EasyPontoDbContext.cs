using System.Data.Entity;
using Dev.Business.Models.Funcionarios;
using Dev.Business.Models.Pontos;
using Dev.Infra.Data.Mappings;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Dev.Infra.Data.Context
{
    public class EasyPontoDbContext : DbContext
    {

        public EasyPontoDbContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

        }

        public DbSet<Ponto> Pontos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p
                .HasColumnType("varchar")
                .HasMaxLength(100));


            modelBuilder.Configurations.Add(new FuncionarioConfig());
            modelBuilder.Configurations.Add(new EnderecoConfig());
            modelBuilder.Configurations.Add(new PontoConfig());

            base.OnModelCreating(modelBuilder);

        }

    }
}
