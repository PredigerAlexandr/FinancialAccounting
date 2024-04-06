using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Payoffs.Queries.GetPayoffListQuery;

public class GetPayoffListQuery : IRequest<IList<Payoff>?>
{
    public string UserEmail { get; set; }
}