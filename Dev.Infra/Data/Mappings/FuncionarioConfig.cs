using Dev.Business.Models.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Infra.Data.Mappings
{
    public class FuncionarioConfig : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioConfig()
        {
            HasKey(f => f.Id);

            Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(f => f.Documento)
                .IsRequired()
                .HasMaxLength(14)
                .HasColumnAnnotation("IX_Documento",
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            HasRequired(f => f.Endereco)
                .WithRequiredPrincipal(e => e.Funcionario);
            
            ToTable("Funcionarios"); 


        }
    }
}
