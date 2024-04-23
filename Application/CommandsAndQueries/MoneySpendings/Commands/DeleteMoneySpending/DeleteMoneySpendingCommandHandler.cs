using Application.Common.Exceptions;
using Application.Debtss.Commands.DeleteUser;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.MoneySpendings.Commands.DeleteMoneySpending;

public class DeleteMoneySpendingCommandHandler : IRequestHandler<DeleteMoneySpendingCommand, int>
{
    private readonly IDbContext _dbContext;
    public DeleteMoneySpendingCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(DeleteMoneySpendingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Debts.Where(l => l.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        _dbContext.Debts.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}