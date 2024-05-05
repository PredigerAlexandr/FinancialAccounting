using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Notifications.Queries.GetNotificationListQuery;

public class GetNotificationListQuery : IRequest<IList<Notification>?>
{
    public string UserEmail { get; set; }
    public int Page { get; set; }
}