using FluentValidation;

namespace Application.CommandsAndQueries.Debts.Commands.CreateDebt;

public class CreateMoneySpendingCommandValidator:AbstractValidator<CreateDebtCommand>
{
    public CreateMoneySpendingCommandValidator()
    {
      
    }
}