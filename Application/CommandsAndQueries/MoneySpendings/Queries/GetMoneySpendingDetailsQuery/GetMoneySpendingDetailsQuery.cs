using Domain.Entities;
using MediatR;

namespace Application.Debtss.Queries.GetDebtsDetails;

public class GetMoneySpendingDetailsQuery : IRequest<MoneySpending>
{
    public Guid Id { get; set; }
}