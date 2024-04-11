using MediatR;

namespace Application.Deposits.Commands.CreateDeposit;

public class CreateDepositCommand : IRequest<int>
{
    public string UserEmail { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public string Name { get; set; }
    public double Rate { get; set; }
}