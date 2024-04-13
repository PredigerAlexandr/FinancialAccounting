using Application.Deposits.Commands.CreateDeposit;
using FluentValidation;

namespace Application.CommandsAndQueries.Payments.Commands.CreatePayment;

public class CreatePaymentCommandValidator:AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
    }
}