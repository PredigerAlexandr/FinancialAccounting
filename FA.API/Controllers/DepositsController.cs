using Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;
using Application.CommandsAndQueries.Deposits.Commands.CreateDeposit;
using Application.Common.Models;
using Application.Debtss.Commands.UpdateUser;
using Application.Deposits.Commands.CreateDeposit;
using Application.Deposits.Commands.DeleteDeposit;
using Application.Deposits.Queries.GetDepositDetails;
using Application.Users.Queries.GetUserDetails;
using AutoMapper;
using Domain.Entities;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;

[Controller]
[Route("[controller]")]
public class DepositsController : BaseController
{
    private readonly IMapper _mapper;

    public DepositsController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{email}/{name}")]
    public async Task<ActionResult<DepositDto>> GetByIdByUserEmail( string email, string name)
    {
        var query = new GetDepositDetailsQuery()
        {
            Name = name,
            EmailUser = email
        };

        var vm = _mapper.Map<DepositDto>(await Mediator.Send(query));

        return Ok(vm);
    }

    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult<BankDeposit>> GetByUserEmail(string email)
    {
        var query = new GetDepositListQuery()
        {
            UserEmail = email
        };

        var vm = _mapper.Map<List<DepositDto>>(await Mediator.Send(query));

        return Ok(vm);
    }


    [HttpPost]
    public async Task<ActionResult<UserDto>> Add([FromBody] CreateDepositCommand createDepositsCommand)
    {
        var vm = await Mediator.Send(createDepositsCommand);
        return Ok(vm);
    }

    [HttpDelete]
    [Route("{email}/{name}")]
    public async Task<ActionResult<DepositDto>> Delete(string name, string email)
    {
        var command = new DeleteDepositCommand()
        {
            Name = name,
            Email = email
        };
        var vm = await Mediator.Send(command);
        return Ok(vm);
    }

    [HttpPut]
    public async Task<ActionResult<UserDto>> Update([FromBody] UpdateDepositCommand updateDepositsCommand)
    {
        var vm = await Mediator.Send(updateDepositsCommand);
        return Ok(vm);
    }
}