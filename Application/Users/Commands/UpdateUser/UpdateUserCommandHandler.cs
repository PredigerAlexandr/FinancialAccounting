using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.Commands.AddUser;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IDbContext _dbContext;
    public UpdateUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FirstOrDefaultAsync(user =>
            user.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        entity.Name = request.Name;
        entity.MiddleName = request.MiddleName;
        entity.Age = request.Age;
        entity.Salary = request.Salary;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}