using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteLoanCommand : IRequest<int>
{
    public int Id { get; set; }
}