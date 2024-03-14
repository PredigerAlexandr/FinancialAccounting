using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Loans.Commands.DeleteUser;

public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, int>
{
    private readonly IDbContext _dbContext;
    public DeleteLoanCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Loans.Where(l => l.Name == request.Name).Where(l => l.User.Email == request.Email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Name);
        }

        _dbContext.Loans.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}