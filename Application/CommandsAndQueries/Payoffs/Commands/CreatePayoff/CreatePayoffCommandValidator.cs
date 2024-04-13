using Application.Deposits.Commands.CreateDeposit;
using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreatePayoffCommandValidator:AbstractValidator<CreatePayoffCommand>
{
    public CreatePayoffCommandValidator()
    {
    }
}