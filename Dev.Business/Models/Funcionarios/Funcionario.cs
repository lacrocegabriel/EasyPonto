using Dev.Business.Core.Models;
using Dev.Business.Models.Pontos;
using System.Collections.Generic;

namespace Dev.Business.Models.Funcionarios
{
    public class Funcionario : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFuncionario TipoFuncionario { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public ICollection<Ponto> Pontos { get; set; }
        




    }
}
