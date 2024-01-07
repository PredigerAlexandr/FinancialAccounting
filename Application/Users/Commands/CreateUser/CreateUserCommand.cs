using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand:IRequest<int>
{
    
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public int? Salary { get; set; }
}