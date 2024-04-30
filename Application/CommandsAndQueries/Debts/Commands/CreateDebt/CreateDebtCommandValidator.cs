using FluentValidation;

namespace Application.CommandsAndQueries.Debts.Commands.CreateDebt;

public class CreateDebtsCommandValidator:AbstractValidator<CreateDebtCommand>
{
    public CreateDebtsCommandValidator()
    {
        var validValues = new[] {"долг", "кредит", "Долг", "Кредит"};
        RuleFor(d=>d.Type)
            .Must(value => validValues.Contains(value))
            .WithMessage("Значение поля Rate должно быть из списка допустимых значений");
        RuleFor(d => d.Rate).GreaterThan(0).When(d=>d.Type.ToLower()=="debt").WithMessage("Если был выбран тип \"Кредит\", то поле Rate обязательно");
        
    }
}