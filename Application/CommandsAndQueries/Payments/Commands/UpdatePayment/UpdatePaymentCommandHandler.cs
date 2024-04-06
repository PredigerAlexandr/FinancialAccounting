using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.Commands.AddUser;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Debtss.Commands.UpdateUser;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdateDebtsCommand, int>
{
    private readonly IDbContext _dbContext;
    public UpdatePaymentCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(UpdateDebtsCommand request, CancellationToken cancellationToken)
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