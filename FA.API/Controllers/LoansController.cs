using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;
[Controller]
[Route("[controller]")]
public class LoansController:BaseController
{
    public LoansController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(string id)
    {
        var query = new GetUserDetailsQuery()
        {
            Id = new Guid(id)
        };

        var vm = await Mediator.Send(query);

        return Ok(vm);
    }
    
    [HttpPut]
    public async Task<ActionResult<UserDto>> Add([FromBody] CreateLoanCommand createLoanCommand)
    {
        var vm = await Mediator.Send(createLoanCommand);
        return Ok(vm);
    }
    
    [HttpDelete]
    public async Task<ActionResult<UserDto>> Delete([FromBody] CreateLoanCommand createLoanCommand)
    {
        var vm = await Mediator.Send(createLoanCommand);
        return Ok(vm);
    }
    
    [HttpPatch]
    public async Task<ActionResult<UserDto>> Update([FromBody] CreateLoanCommand createLoanCommand)
    {
        var vm = await Mediator.Send(createLoanCommand);
        return Ok(vm);
    }
    
}