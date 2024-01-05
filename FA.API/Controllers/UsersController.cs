using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;

[ApiController]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var query = new GetUserDetailsQuery()
        {
            Id = id
        };

        var vm = await Mediator.Send(query);

        return Ok(vm);
    }
    
    [HttpPut]
    public async Task<ActionResult<ICollection<UserDto>>> Add([FromBody] CreateUserCommand createUserCommand)
    {
        var vm = Mediator.Send(createUserCommand);
        return Ok(vm);
    }
}