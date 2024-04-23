using MediatR;

namespace Application.CommandsAndQueries.Debts.Commands.CreateDebt;

public record CreateMoneySpendingCommand : IRequest<int>
{
    public string Type { get; init; }
    public int Amount { get; init; }
    public DateTime Date { get; init; }
    public string UserEmail { get; init; }
}