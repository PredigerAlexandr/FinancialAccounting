using MediatR;

namespace Application.Users.Commands.AddUser;

public class UpdateLoanCommand : IRequest<Guid>, IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
}