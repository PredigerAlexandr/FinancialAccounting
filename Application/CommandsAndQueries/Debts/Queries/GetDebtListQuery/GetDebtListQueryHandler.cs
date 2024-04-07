using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;

public class GetDebtsListQueryHandler : IRequestHandler<GetDebtListQuery, IList<Debt>?>
{
    private readonly IDbContext _dbContext; 
    private readonly IMapper _mapper;

    public GetDebtsListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    

    public async Task<IList<Debt>> Handle(GetDebtListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Debts.Where(u => u.User.Email == request.UserEmail)
            .ToListAsync(cancellationToken);

        return entities;
    }
}