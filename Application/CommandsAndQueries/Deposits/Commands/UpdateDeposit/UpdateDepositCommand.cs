using MediatR;

namespace Application.Debtss.Commands.UpdateUser;

public class UpdateDepositCommand : IRequest<int>
{
    public string OldName { get; set; }
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public decimal Rate { get; set; }
}