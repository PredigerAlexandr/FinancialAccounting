using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Payments.Queries.GetDepositListQuery;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Deposits.Queries.GetDepositListQuery;

public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQuery, IList<Payment>?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaymentListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<IList<Payment>?> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Payments.Where(e => e.Debt.User.Email == request.UserEmail)
            .ToListAsync(cancellationToken: cancellationToken);

        return entities;
    }
}