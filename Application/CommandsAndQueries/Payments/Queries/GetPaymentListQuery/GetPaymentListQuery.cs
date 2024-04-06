using Domain.Entities;
using MediatR;

namespace Application.Payments.Queries.GetDepositListQuery;

public class GetPaymentListQuery : IRequest<IList<Payment>?>
{
    public string UserEmail { get; set; }
}