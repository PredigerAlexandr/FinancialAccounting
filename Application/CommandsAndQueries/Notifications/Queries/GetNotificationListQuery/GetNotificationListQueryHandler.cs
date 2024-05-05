using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Notifications.Queries.GetNotificationListQuery;

public class GetNotificationListQueryHandler : IRequestHandler<GetNotificationListQuery, IList<Notification>?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNotificationListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<IList<Notification>?> Handle(GetNotificationListQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Notifications.Where(n => n.User.Email == request.UserEmail).OrderBy(n => n.Date)
            .Skip((request.Page-1) * 10).Take(10)
            .ToListAsync(cancellationToken: cancellationToken);

        return entities;
    }
}