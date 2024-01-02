using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand, int>
{
    private readonly IDbContext _dbContext;
    public DeleteUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;
    public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        _dbContext.Users.Remove(entity);

        return request.Id;
    }
}