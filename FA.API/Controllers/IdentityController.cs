using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Models;
using Application.Services.IdentityService;
using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AutoMapper;
using Domain.Models.JWT;
using Microsoft.IdentityModel.Tokens;

namespace CreditAPI.Controllers;

/// <summary>
/// Сервис аутентификации, авторизации и идентификации 
/// </summary>
[ApiController]
[Route("[controller]")]
public class IdentityController:BaseController
{
    private readonly IIdentityService _identityService;
    private const string SecretKey = "95381538c4da5c17ea6a4a9e19de7258";

    public IdentityController(IMediator mediator, IMapper mapper, IIdentityService identityService) : base(mediator, mapper)
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

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };
        var jwt = new JwtSecurityToken(issuer: JwtOptions.ISSUER,
            audience: JwtOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.KEY)), SecurityAlgorithms.HmacSha256)
        );
        var result = new JwtSecurityTokenHandler().WriteToken(jwt);
                
        return  Ok(new {name = user.Name, jwt=result});
    }
    
    
}