using MediatR;

namespace Application.Users.Commands.AddUser;

public class UpdateUserCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public int? Salary { get; set; }
}