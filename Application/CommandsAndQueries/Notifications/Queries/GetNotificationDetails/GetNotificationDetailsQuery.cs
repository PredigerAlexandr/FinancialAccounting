using Domain.Entities;
using MediatR;

namespace Application.CommandsAndQueries.Notifications.Queries.GetNotificationDetails;

public class GetNotificationDetailsQuery : IRequest<Notification?>
{
    public Guid Id { get; set; }
}