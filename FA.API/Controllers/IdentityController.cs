using Application.Common.Models;
using Application.Services.IdentityService;
using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
namespace CreditAPI.Controllers;

/// <summary>
/// Сервис аутентификации, авторизации и идентификации 
/// </summary>
[ApiController]
[Route("[controller]")]
public class IdentityController:BaseController
{
    private readonly IIdentityService _identityService;
    private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private const string SecretKey = "95381538c4da5c17ea6a4a9e19de7258";

    public IdentityController(IMediator mediator, IIdentityService identityService) : base(mediator)
    {
        _identityService = identityService;
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
        createUserCommand.Salt = SecretKey;
        var vm = await Mediator.Send(createUserCommand);
        return Ok(vm);
    }
    
    public async Task<ActionResult> Login([FromBody] UserLoginDto loginUserDto)
    {
        
        createUserCommand.Salt = SecretKey;
        var vm = await Mediator.Send(createUserCommand);
        return Ok(vm);
    }
    
    
}