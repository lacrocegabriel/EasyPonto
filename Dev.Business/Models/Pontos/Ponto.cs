using Dev.Business.Core.Models;
using Dev.Business.Models.Funcionarios;
using System;

namespace Dev.Business.Models.Pontos
{
    public class Ponto : Entity
    {
        public Guid FuncionarioId { get; set; }
        public DateTime DataPonto { get; set; }

        /* EF Relations */

        public Funcionario Funcionario { get; set; }


    }
}
