using Application.Common.Models;
using Application.Loans.Commands.CreateLoan;
using Application.Loans.Commands.DeleteUser;
using Application.Loans.Commands.UpdateUser;
using Application.Loans.Queries.GetLoanDetails;
using Application.Users.Commands.AddUser;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserDetails;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;
[Controller]
[Route("[controller]")]
public class LoansController:BaseController
{
    private readonly IMapper _mapper;
    public LoansController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{name}/{email}")]
    public async Task<ActionResult<UserDto>> GetById(string name, string email)
    {
        var query = new GetLoanDetailsQuery()
        {
            Name = name,
            EmailUser = email
        };

        var vm = _mapper.Map<LoanDto>(await Mediator.Send(query));

        return Ok(vm);
    }
    
    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult<UserDto>> GetByUserEmail(string email)
    {
        var query = new GetLoanListQuery()
        {
            UserEmail = email
        };

        var vm = _mapper.Map<List<LoanDto>>(await Mediator.Send(query));

        return Ok(vm);
    }
    
    
    [HttpPut]
    public async Task<ActionResult<UserDto>> Add([FromBody] CreateLoanCommand createLoanCommand)
    {
        var vm = await Mediator.Send(createLoanCommand);
        return Ok(vm);
    }
    
    [HttpDelete]
    [Route("{name}/{email}")]
    public async Task<ActionResult<UserDto>> Delete(string name, string email)
    {
        var command = new DeleteLoanCommand()
        {
            Name = name,
            Email = email
        };
        var vm = await Mediator.Send(command);
        return Ok(vm);
    }
    
    [HttpPatch]
    public async Task<ActionResult<UserDto>> Update([FromBody] UpdateLoanCommand updateLoanCommand)
    {
        var vm = await Mediator.Send(updateLoanCommand);
        return Ok(vm);
    }
    
}