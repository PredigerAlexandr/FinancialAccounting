using MediatR;

namespace Application.CommandsAndQueries.MoneySpendings.Commands.DeleteMoneySpending;

public class DeleteMoneySpendingCommand : IRequest<int>
{
    public Guid Id { get; set; }
}