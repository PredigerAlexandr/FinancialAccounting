using Application.Common.Exceptions;
using Application.Debtss.Commands.DeleteUser;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Deposits.Commands.DeleteDeposit;

public class DeleteDepositCommandHandler : IRequestHandler<DeleteDebtsCommand, int>
{
    private readonly IDbContext _dbContext;
    public DeleteDepositCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(DeleteDebtsCommand request, CancellationToken cancellationToken)
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