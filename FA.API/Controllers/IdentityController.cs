using Application.Common.Filters;
using Application.Common.Models;
using Application.Services;
using Application.Services.IdentityService;
using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;
[ApiController]
public class IdentityController:BaseController
{
    private readonly IIdentityService _identityService; 
    public IdentityController(IMediator mediator, IIdentityService identityService) : base(mediator)
    {
        _identityService = identityService;
    }

    [HttpGet]
    [Route("/checkLogin")]
    public ActionResult<string> CheckLogin()
    {
        return Ok(_identityService.GenerateChallenge());
    }
    
    [Route("/registration")]
    [HttpPut]
    public async Task<ActionResult<UserDto>> Registration([FromBody] CreateUserCommand createUserCommand)
    {
        var hui = _identityService.Challenge;
        var vm = await Mediator.Send(createUserCommand);
        return Ok(vm);
    }
}