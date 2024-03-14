using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.Commands.AddUser;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Loans.Commands.UpdateUser;

public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, int>
{
    private readonly IDbContext _dbContext;
    public UpdateLoanCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Loans.FirstOrDefaultAsync(loan =>
            loan.Name == request.OldName, cancellationToken);

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