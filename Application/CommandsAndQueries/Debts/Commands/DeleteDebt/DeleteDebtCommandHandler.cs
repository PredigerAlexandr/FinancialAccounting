using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Debtss.Commands.DeleteUser;

public class DeleteDebtsCommandHandler : IRequestHandler<DeleteDebtCommand, int>
{
    private readonly IDbContext _dbContext;
    public DeleteDebtsCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(DeleteDebtCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Debts.Where(l => l.Name == request.Name).Where(l => l.User.Email == request.Email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Name);
        }

        _dbContext.Debts.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}