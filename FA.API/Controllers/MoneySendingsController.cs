using System.Reflection.Metadata;
using Application.CommandsAndQueries.Users.Queries.GetUserDetails;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Constants = Application.Common.Constants;

namespace CreditAPI.Controllers;

[Controller]
[Route("[controller]")]
public class MoneySendingsController : BaseController
{
    public MoneySendingsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    [Route("types/{userEmail}")]
    public async Task<ActionResult<string[]>> GetTypes(string userEmail)
    {
        var query = new GetUserDetailsQuery()
        {
            Email = userEmail
        };
        var user = await Mediator.Send(query);

        return Ok(user.IsAuto is true
            ? Constants.Constant.TypeMoneySendingsForAuto.Concat(Constants.Constant.TypeMoneySendings)
            : Constants.Constant.TypeMoneySendings);
    }
}