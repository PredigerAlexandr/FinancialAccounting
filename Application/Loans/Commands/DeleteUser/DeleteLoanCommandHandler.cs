using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteLoanCommandHandler:IRequestHandler<DeleteLoanCommand, int>
{
    private readonly IDbContext _dbContext;
    public DeleteLoanCommandHandler(IDbContext dbContext) => _dbContext = dbContext;
    public async Task<int> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Loans.FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        _dbContext.Loans.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}