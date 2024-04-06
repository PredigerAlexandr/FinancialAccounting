using Domain.Entities;
using MediatR;

namespace Application.Deposits.Queries.GetDepositDetails;

public class GetPaymentDetailsQuery : IRequest<BankDeposit>
{
    public string Name { get; set; }
    public string EmailUser { get; set; }
}