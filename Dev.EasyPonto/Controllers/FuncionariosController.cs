using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dev.Business.Models.Funcionarios;
using Dev.Business.Models.Funcionarios.Services;
using Dev.Infra.Data.Repository;

namespace Dev.EasyPonto.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly IFuncionarioService _funcioarioService;

        //public FuncionariosController()
        //{
        //    _funcioarioService = new FuncionarioService(new FuncionarioRepository(), new EnderecoRepository());
        //}

        public async Task<ActionResult> Index()
        {
            var funcionario = new Funcionario()
            {
                Nome = "Gabriel Lacroce",
                Documento = "10722258917",
                Endereco = new Endereco
                {
                    Logradouro = "Rua Antonio Ramos Valença",
                    Bairro = "Belleville",
                    Numero = "57",
                    Cidade = "Londrina",
                    Estado = "PR",
                    Cep = "86084330",
                    Complemento = "Casa 5"
                    
                },
                TipoFuncionario = TipoFuncionario.CLT,
                Ativo = true
            };

            await _funcioarioService.Adicionar(funcionario);

            return new EmptyResult();
        }
    

    }
}