using Application.CommandsAndQueries.Users.Queries.GetUserDetails;
using Application.Common.Models;
using Application.Users.Commands.AddUser;
using Application.Users.Commands.CreateUser;
using AutoMapper;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<UserDto>> GetById(string id)
    // {
    //     var query = new GetUserDetailsQuery()
    //     {
    //         Id = new Guid(id)
    //     };
    //
    //     var vm = await Mediator.Send(query);
    //
    //     return Ok(vm);
    // }
    
    [HttpGet]
    [Route("user-info/{userEmail}")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfo(string userEmail)
    {
        var query = new GetUserDetailsQuery()
        {
            Email = userEmail
        };
        var entity = await Mediator.Send(query);
        var result = Mapper.Map<UserInfoDto>(entity);
        return Ok(result);
    }
    
    [HttpPut]
    [Route("user-info")]
    public async Task<ActionResult<UserInfoDto>>UpdateUserInfo(UpdateUserCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }
    
    [HttpPut]
    [Route("add")]
    public async Task<ActionResult<UserDto>> Add([FromBody] CreateUserCommand createUserCommand)
    {
        var vm = await Mediator.Send(createUserCommand);
        return Ok(vm);
    }
}