using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Payoffs.Queries.GetPayoffsPerMonth;

public class GetPayoffsPerMonthQuery : IRequest<IList<Payoff>?>
{
    public string UserEmail { get; set; }
    public DateTime Date { get; set; }
}