using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateStatisticCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateStatisticCommandValidator()
    {
    }
}