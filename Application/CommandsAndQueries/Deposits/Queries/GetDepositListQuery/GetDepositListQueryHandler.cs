using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Deposits.Queries.GetDepositListQuery;

public class
    GetDepositListQueryHandler : IRequestHandler<Users.Queries.GetUserDetails.GetDepositListQuery, IList<BankDeposit>?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetDepositListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<IList<BankDeposit>?> Handle(Users.Queries.GetUserDetails.GetDepositListQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _dbContext.BankDeposits.Where(u => u.User.Email == request.UserEmail)
            .ToListAsync(cancellationToken);

        return entities;
    }
}