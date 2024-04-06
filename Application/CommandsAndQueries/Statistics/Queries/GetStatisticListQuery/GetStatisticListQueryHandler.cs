using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.Queries.GetUserDetails;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Debtss.Queries.GetDebtsListQuery;

public class GetStatisticListQueryHandler : IRequestHandler<GetStatisticListQuery, IList<Statistic>?>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetStatisticListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<IList<Statistic>?> Handle(GetStatisticListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Statistics.Where(u => u.User.Email == request.UserEmail)
            .ToListAsync(cancellationToken);

        return entities;
    }
}