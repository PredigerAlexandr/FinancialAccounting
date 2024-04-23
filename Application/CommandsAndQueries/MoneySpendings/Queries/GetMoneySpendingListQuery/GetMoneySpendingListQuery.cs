using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListQuery;

public class GetMoneySpendingListQuery : IRequest<IList<MoneySpending>?>
{
    public string UserEmail { get; set; }
}