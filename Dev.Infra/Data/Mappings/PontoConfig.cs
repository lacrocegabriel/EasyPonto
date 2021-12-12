using Dev.Business.Models.Pontos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Infra.Data.Mappings
{
    public class PontoConfig : EntityTypeConfiguration<Ponto>
    {
        public PontoConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.DataPonto)
                .IsRequired();

            HasRequired(p => p.Funcionario)
                .WithMany(f => f.Pontos)
                .HasForeignKey(p => p.FuncionarioId);

            ToTable("Pontos"); 
            
        }


    }
}
