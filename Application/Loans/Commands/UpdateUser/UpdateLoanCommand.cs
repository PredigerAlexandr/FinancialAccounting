using MediatR;

namespace Application.Loans.Commands.UpdateUser;

public class UpdateLoanCommand : IRequest<int>
{
    public string OldName { get; set; }
    public string Name { get; set; }
    public decimal FullSum { get; set; }
    public decimal CurrentSum { get; set; }
    public decimal Rate { get; set; }
}