using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;

public class GetMoneySpendingListByMonthQuery : IRequest<IList<MoneySpending>?>
{
    public string UserEmail { get; set; }
    public DateTime Date { get; set; }
}