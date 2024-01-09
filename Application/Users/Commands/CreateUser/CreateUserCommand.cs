using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public int? Salary { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatedPassword { get; set; }
    
}