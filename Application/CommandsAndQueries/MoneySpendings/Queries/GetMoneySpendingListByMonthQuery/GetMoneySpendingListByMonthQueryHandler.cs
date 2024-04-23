using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;

public class GetMoneySpendingListByMonthQueryHandler : IRequestHandler<GetMoneySpendingListByMonthQuery, IList<MoneySpending>?>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetMoneySpendingListByMonthQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<IList<MoneySpending>?> Handle(GetMoneySpendingListByMonthQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.MoneySpendings.Where(u => u.User.Email == request.UserEmail)
            .Where(u=>u.Date.Year == request.Date.Year && u.Date.Month==request.Date.Month)
            .ToListAsync(cancellationToken);

        return entities;
    }
}