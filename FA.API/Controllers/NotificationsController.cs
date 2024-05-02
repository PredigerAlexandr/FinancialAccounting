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
    [Route("{userEmail}")]
    public async Task<ActionResult<List<Notification>?>> GetNotificationList(
         string userEmail)
    {
        var entites = await Mediator.Send(new GetNotificationListQuery()
        {
            UserEmail = userEmail
        });
        return Ok(entites);
    }
}