using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;
using Application.CommandsAndQueries.Notifications.Queries.GetNotificationDetails;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandsAndQueries.Notifications.Queries.GetNotificationListQuery;

public class GetNotificationDetailsQueryHandler : IRequestHandler<GetNotificationDetailsQuery, Notification?>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNotificationDetailsQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Notification?> Handle(GetNotificationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notifications.Where(n => n.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return entity;
    }
}