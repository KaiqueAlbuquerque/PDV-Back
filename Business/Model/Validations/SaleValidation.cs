using FluentValidation;

namespace Business.Model.Validations
{
    public class SaleValidation : AbstractValidator<Sale>
    {
        public SaleValidation()
        {
            RuleFor(s => s.TotalValue)
                .NotEmpty().WithMessage("O Valor Total precisa ser fornecido.");

            RuleFor(s => s.FormOfPayment)
                .NotEmpty().WithMessage("A Forma de Pagamento precisa ser fornecido.");

            RuleFor(s => s.AmountPaid)
                .NotEmpty().WithMessage("O Valor Pago precisa ser fornecido.");

            RuleFor(s => s.Change)
                .NotEmpty().WithMessage("O Troco precisa ser fornecido.");
        }
    }
}
