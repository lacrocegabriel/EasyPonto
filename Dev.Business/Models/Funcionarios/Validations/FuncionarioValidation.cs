using FluentValidation;
using Dev.Business.Core.Validations.Documentos;

namespace Dev.Business.Models.Funcionarios.Validations
{
    public class FuncionarioValidation : AbstractValidator <Funcionario>
    {
        public FuncionarioValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo deve possuir no mínimo {MinLength} e no máximo {MaxLength} caracteres");

            When(f => f.TipoFuncionario == TipoFuncionario.CLT, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("O campo Documento preicsa ter {ComparisionValue} caracteres e foi fornecido {PropertyValue}");
                
                RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido");

            });
            When(f => f.TipoFuncionario == TipoFuncionario.PJ, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                .WithMessage("O campo Documento preicsa ter {ComparisionValue} caracteres e foi fornecido {PropertyValue}");
                
                RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido");
            });

        }

    }
}
