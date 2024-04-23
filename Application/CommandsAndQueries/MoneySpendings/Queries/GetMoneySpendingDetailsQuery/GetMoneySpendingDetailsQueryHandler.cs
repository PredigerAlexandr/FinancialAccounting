using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Debtss.Queries.GetDebtsDetails;

public class GetMoneySpendingDetailsQueryHandler : IRequestHandler<GetMoneySpendingDetailsQuery, MoneySpending>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMoneySpendingDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<MoneySpending> Handle(GetMoneySpendingDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.MoneySpendings.Where(u => u.Id == request.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return entity;
    }
}