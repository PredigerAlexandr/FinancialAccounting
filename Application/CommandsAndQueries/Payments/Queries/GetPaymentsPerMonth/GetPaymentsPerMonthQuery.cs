using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Payments.Queries.GetPaymentsPerMonth;

public class GetPaymentsPerMonthQuery : IRequest<IList<Payment>?>
{
    public string UserEmail { get; set; }
    public DateTime Date { get; set; }
}