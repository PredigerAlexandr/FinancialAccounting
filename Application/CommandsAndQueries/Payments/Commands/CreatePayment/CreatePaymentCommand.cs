using MediatR;

namespace Application.CommandsAndQueries.Payments.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<int>
{
   public string UserEmail { get; set; }
   public string DebtName { get; set; }
   public decimal Amount { get; set; }
}