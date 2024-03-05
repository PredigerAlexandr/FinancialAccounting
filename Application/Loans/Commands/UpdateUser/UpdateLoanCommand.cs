using MediatR;

namespace Application.Users.Commands.AddUser;

public class UpdateLoanCommand : IRequest<Guid>, IRequest<int>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}