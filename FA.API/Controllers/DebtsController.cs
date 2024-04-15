using Application.CommandsAndQueries.Debts.Commands.CreateDebt;
using Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;
using Application.Common.Models;
using Application.Debtss.Commands.CreateDebts;
using Application.Debtss.Commands.DeleteUser;
using Application.Debtss.Commands.UpdateUser;
using Application.Debtss.Queries.GetDebtsDetails;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;
[Controller]
[Route("[controller]")]
public class DebtsController:BaseController
{
    private readonly IMapper _mapper;
    public DebtsController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{name}/{email}")]
    public async Task<ActionResult<UserDto>> GetById(string name, string email)
    {
        var query = new GetDebtsDetailsQuery()
        {
            Name = name,
            EmailUser = email
        };

        var vm = _mapper.Map<DebtDto>(await Mediator.Send(query));

        return Ok(vm);
    }
    
    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult<IList<DebtDto>>> GetByUserEmail(string email)
    {
        var query = new GetDebtListQuery()
        {
            UserEmail = email
        };

        var vm = _mapper.Map<List<DebtDto>>(await Mediator.Send(query));

        return Ok(vm);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> Add([FromBody] CreateDebtCommand createDebtsCommand)
    {
        var vm = await Mediator.Send(createDebtsCommand);
        return Ok(vm);
    }
    
    [HttpDelete]
    [Route("{email}/{name}")]
    public async Task<ActionResult<UserDto>> Delete(string name, string email)
    {
        var command = new DeleteDebtCommand()
        {
            Name = name,
            Email = email
        };
        var vm = await Mediator.Send(command);
        return Ok(vm);
    }
    
    [HttpPatch]
    public async Task<ActionResult<UserDto>> Update([FromBody] UpdateDebtsCommand updateDebtsCommand)
    {
        var vm = await Mediator.Send(updateDebtsCommand);
        return Ok(vm);
    }
    
}