using MediatR;

namespace Application.Loans.Commands.DeleteUser;

public class DeleteLoanCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
}