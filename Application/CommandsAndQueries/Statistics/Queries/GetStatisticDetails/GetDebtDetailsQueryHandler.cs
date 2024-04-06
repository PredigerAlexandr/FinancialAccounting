using Application.CommandsAndQueries.Statistics.Queries.GetStatisticDetails;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Debtss.Queries.GetDebtsDetails;

public class GetLastStatisticDetailsQueryHandler : IRequestHandler<GetLastStatisticDetailsQuery, Statistic?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetLastStatisticDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Statistic?> Handle(GetLastStatisticDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Statistics.Where(e => e.User.Email == request.EmailUser)
            .OrderBy(u => u.CreateDate)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return entity;
    }
}