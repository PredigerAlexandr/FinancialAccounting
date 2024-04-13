using FluentValidation;

namespace Application.CommandsAndQueries.Deposits.Commands.CreateDeposit;

public class CreateDepositCommandValidator:AbstractValidator<CreateDepositCommand>
{
    public CreateDepositCommandValidator()
    {
    }
}