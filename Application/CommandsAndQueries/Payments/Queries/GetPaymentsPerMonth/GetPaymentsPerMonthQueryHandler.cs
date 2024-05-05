using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;
using Application.CommandsAndQueries.Payments.Queries.GetPaymentsPerMonth;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Payoffs.Queries.GetPayoffsPerMonth;

public class GetPaymentsPerMonthQueryHandler : IRequestHandler<GetPaymentsPerMonthQuery, IList<Payment>?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaymentsPerMonthQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<IList<Payment>?> Handle(GetPaymentsPerMonthQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Payments.Where(u => u.Debt.User.Email == request.UserEmail)
            .Where(u => u.DateCreate.Year == request.Date.Year && u.DateCreate.Month == request.Date.Month)
            .ToListAsync(cancellationToken);

        return entities;
    }
}