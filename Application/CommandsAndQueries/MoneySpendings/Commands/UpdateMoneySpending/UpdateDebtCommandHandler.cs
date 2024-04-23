using Application.Common.Exceptions;
using Application.Debtss.Commands.UpdateUser;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.MoneySpendings.Commands.UpdateMoneySpending;

public class UpdateMoneySpendingCommandHandler : IRequestHandler<UpdateMoneySpendingCommand, int>
{
    private readonly IDbContext _dbContext;
    public UpdateMoneySpendingCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(UpdateMoneySpendingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Debts.FirstOrDefaultAsync(Debts =>
            Debts.Name == request.OldName, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.OldName);
        }

        entity.Name = request.Name;
        entity.CurrentSum = request.CurrentSum;
        entity.Rate = request.Rate;
        entity.FullSum = request.FullSum;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}