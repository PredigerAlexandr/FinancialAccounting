using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;

public class GetDebtListQuery : IRequest<IList<Debt>?>
{
    public string UserEmail { get; set; }
}