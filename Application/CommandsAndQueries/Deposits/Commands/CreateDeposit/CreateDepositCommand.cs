using MediatR;

namespace Application.CommandsAndQueries.Deposits.Commands.CreateDeposit;

public class CreateDepositCommand : IRequest<int>
{
    public string UserEmail { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public bool Capitalization { get; set; }
    public string Name { get; set; }
    public double Rate { get; set; }
    public DateTime DateStart { get; set; }
    public int MonthsTotal { get; set; }
}