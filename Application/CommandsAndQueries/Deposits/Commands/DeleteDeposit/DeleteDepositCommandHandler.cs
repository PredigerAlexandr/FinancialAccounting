using Application.Common.Exceptions;
using Application.Debtss.Commands.DeleteUser;
using Application.Deposits.Commands.DeleteDeposit;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Deposits.Commands.DeleteDeposit;

public class DeleteDepositCommandHandler : IRequestHandler<DeleteDepositCommand, int>
{
    private readonly IDbContext _dbContext;
    public DeleteDepositCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(DeleteDepositCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.BankDeposits.Where(d => d.Name == request.Name).Where(d => d.User.Email == request.Email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Name);
        }

        _dbContext.BankDeposits.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return 1;
    }
}