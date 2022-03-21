using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dev.EasyPonto.Extensions;

namespace Dev.EasyPonto.ViewModels
{
    public class PontoViewModel
    {
        public PontoViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required (ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Funcionario")]
        public Guid FuncionarioId { get; set; }

        [Data]
        [Required(ErrorMessage = "A data e horario devem ser informadas")]
        public DateTime DataPonto { get; set; }

        public FuncionarioViewModel Funcionario { get; set; }

        public IEnumerable<FuncionarioViewModel> Funcionarios { get; set; }

    }
}