using Application.Debtss.Commands.CreateDebts;
using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateDebtsCommandValidator:AbstractValidator<CreateDebtsCommand>
{
    public CreateDebtsCommandValidator()
    {
    }
}