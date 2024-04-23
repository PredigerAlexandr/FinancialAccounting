using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListQuery;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;

public class GetMoneySpendingListQueryHandler : IRequestHandler<GetMoneySpendingListQuery, IList<MoneySpending>?>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetMoneySpendingListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<IList<MoneySpending>?> Handle(GetMoneySpendingListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.MoneySpendings.Where(u => u.User.Email == request.UserEmail)
            .ToListAsync(cancellationToken);

        return entities;
    }
}