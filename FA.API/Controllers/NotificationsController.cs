using Application.CommandsAndQueries.Notifications.Queries.GetNotificationDetails;
using Application.CommandsAndQueries.Notifications.Queries.GetNotificationListQuery;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;

[Controller]
[Route("[controller]")]
public class NotificationsController : BaseController
{
    public NotificationsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    [Route("{page}/{userEmail}")]
    public async Task<ActionResult<List<Notification>?>> GetNotificationList(
         string userEmail, int page)
    {
        var entites = await Mediator.Send(new GetNotificationListQuery()
        {
            UserEmail = userEmail,
            Page = page
        });
        return Ok(entites);
    }
    
    [HttpGet]
    [Route("{guid}")]
    public async Task<ActionResult<Notification?>> GetNotification(string guid)
    {
        var entity = await Mediator.Send(new GetNotificationDetailsQuery()
        {
            Id = new Guid(guid)
        });
        return Ok(entity);
    }
}