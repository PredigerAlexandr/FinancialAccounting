using System.Security.Claims;
using Application.Common.Models;
using Application.Services.IdentityService;
using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Application.Services.UserService;

namespace CreditAPI.Controllers;

/// <summary>
/// Сервис аутентификации, авторизации и идентификации 
/// </summary>
[ApiController]
[Route("[controller]")]
public class IdentityController:BaseController
{
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;
    private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private const string SecretKey = "95381538c4da5c17ea6a4a9e19de7258";

    public IdentityController(IMediator mediator, IIdentityService identityService, IUserService userService) : base(mediator)
    {
        _identityService = identityService;
        _userService = userService;
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
    [HttpPost]
    [Route("/login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _identityService.LoginUserAsync(userLoginDto, SecretKey);
        if (user == null)
        {
            return BadRequest(JsonSerializer.Serialize(
                new Dictionary<string, string> { { "error", "Not found this user." } }
            ));
        }
                
        return Ok();
    }
    
    
}