using FluentValidation;

namespace Business.Model.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.ExternalId)
                .NotEmpty().WithMessage("O campo Cód. precisa ser fornecido.");

            RuleFor(p => p.BarCode)
                .NotEmpty().WithMessage("O campo Código de Barras precisa ser fornecido.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("O campo Descrição precisa ser fornecido.");

            /*RuleFor(p => p.NoteDescription)
                .NotEmpty().WithMessage("O campo Descrição na Nota precisa ser fornecido.")
                .Length(1, 17).WithMessage("O campo Descrição na Nota precisa ter entre {MinLength} e {MaxLength} caracteres.");*/

            /*RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("O campo Quantidade precisa ser fornecido.");*/
            
            RuleFor(p => p.MeasuredUnit)
                .NotEmpty().WithMessage("O campo Uni. precisa ser fornecido.");
        }
    }
}
