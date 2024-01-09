using Application.Common.Models;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            MiddleName = request.MiddleName,
            Surname = request.Surname,
            Age = request.Age,
            Salary = request.Salary,
            Email = request.Email,
            Password = request.Password
        };

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<User, UserDto>(user);
        
    }
}