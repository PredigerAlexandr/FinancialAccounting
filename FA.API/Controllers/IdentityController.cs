using Application.Common.Filters;
using Application.Common.Models;
using Application.Services;
using Application.Services.IdentityService;
using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;

/// <summary>
/// Сервис аутентификации, авторизации и идентификации 
/// </summary>
[ApiController]
public class IdentityController:BaseController
{
    private readonly IIdentityService _identityService; 
    public IdentityController(IMediator mediator, IIdentityService identityService) : base(mediator)
    {
        _identityService = identityService;
    }
    /// <summary>
    /// действие, которое под видом проверки логина отправлет на клиент соль
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/checkLogin")]
    public ActionResult<string> CheckLogin()
    {
        return Ok(_identityService.GenerateChallenge());
    }
    
    /// <summary>
    /// действие, которое отвечает за регистрацию пользователя
    /// </summary>
    /// <param name="createUserCommand">объект пользователя из тела запроса</param>
    /// <returns></returns>
    [Route("/registration")]
    [HttpPut]
    public async Task<ActionResult<UserDto>> Registration([FromBody] CreateUserCommand createUserCommand)
    {
        var hui = _identityService.Challenge;
        var vm = await Mediator.Send(createUserCommand);
        return Ok(vm);
    }
}