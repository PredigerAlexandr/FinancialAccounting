using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Payoffs.Queries.GetPayoffsPerMonth;

public class GetPayoffsPerMonthQueryHandler : IRequestHandler<GetPayoffsPerMonthQuery, IList<Payoff>?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPayoffsPerMonthQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<IList<Payoff>?> Handle(GetPayoffsPerMonthQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Payoffs.Where(u => u.BankDeposit.User.Email == request.UserEmail)
            .Where(u => u.DateCreate.Year == request.Date.Year && u.DateCreate.Month == request.Date.Month)
            .ToListAsync(cancellationToken);

        return entities;
    }
}