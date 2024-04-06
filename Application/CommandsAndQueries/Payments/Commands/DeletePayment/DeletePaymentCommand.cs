using MediatR;

namespace Application.Deposits.Commands.DeleteDeposit;

public class DeletePaymentCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
}