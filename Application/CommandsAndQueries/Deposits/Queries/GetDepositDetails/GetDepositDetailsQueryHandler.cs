using Application.Common.Exceptions;
using Application.Debtss.Queries.GetDebtsDetails;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Deposits.Queries.GetDepositDetails;

public class GetDepositDetailsQueryHandler : IRequestHandler<GetDepositDetailsQuery, BankDeposit>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetDepositDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<BankDeposit> Handle(GetDepositDetailsQuery request, CancellationToken cancellationToken)
    {
        // var user = await _dbContext.Users.Where(u => u.Email == request.EmailUser)
        //     .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        var entity = await _dbContext.BankDeposits.Where(u => u.Name == request.Name)
            .Where(u => u.User.Email == request.EmailUser).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Name);
        }

        return entity;
    }
}