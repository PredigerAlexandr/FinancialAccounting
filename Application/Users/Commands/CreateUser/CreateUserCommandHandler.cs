using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IDbContext _dbContext;
    public CreateUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            MiddleName = request.MiddleName,
            Surname = request.Surname,
            Age = request.Age,
            Salary = request.Salary,
        };

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}