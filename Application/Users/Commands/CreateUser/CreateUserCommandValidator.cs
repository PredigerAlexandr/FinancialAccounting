using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(createUserCommand => createUserCommand.Name).NotNull()
            .WithMessage("Данное поле обязательно для заполнения");
        RuleFor(createUserCommand => createUserCommand.Name).Must(n => n.All(Char.IsLetter))
            .WithMessage("Данное поле не может содержать цифры");
        RuleFor(createUserCommand => createUserCommand.Age).GreaterThan(14).LessThan(120)
            .WithMessage("Данное поле заполнено некорректно, пользователь не может быть моложе 14 и старше 120 лет.");
        RuleFor(createUserCommand => createUserCommand.Password)
            .Equal(createUserCommand => createUserCommand.RepeatedPassword).WithMessage("Пароли должны совпадать");
    }
}