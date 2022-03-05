using FluentValidation;

namespace Dev.Business.Models.Pontos.Validations
{
    public class PontoValidation : AbstractValidator<Ponto>
    {
        public PontoValidation()
        {
            RuleFor(c => c.DataPonto)
                .NotEmpty().WithMessage("o campo {PropertyName} precisa ser fornecido");
                
        }


    }
}
