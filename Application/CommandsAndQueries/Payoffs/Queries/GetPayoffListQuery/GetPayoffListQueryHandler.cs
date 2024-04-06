using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Payoffs.Queries.GetPayoffListQuery;

public class GetPayoffListQueryHandler : IRequestHandler<GetPayoffListQuery, IList<Payoff>?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPayoffListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<IList<Payoff>?> Handle(GetPayoffListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Payoffs.Where(e => e.BankDeposit.User.Email == request.UserEmail)
            .ToListAsync(cancellationToken: cancellationToken);

        return entities;
    }
}