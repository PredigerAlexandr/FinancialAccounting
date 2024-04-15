using MediatR;

namespace Application.CommandsAndQueries.Debts.Commands.CreateDebt;

public record CreateDebtCommand : IRequest<int>
{
    public string UserEmail { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public string Name { get; set; }
    public double Rate { get; set; }
    public string Type { get; set; }
    public DateTime DateStart { get; set; }
    public int MonthsTotal { get; set; }
}