using Application.CommandsAndQueries.Debts.Commands.CreateDebt;
using Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;
using Application.CommandsAndQueries.Payments.Commands.CreatePayment;
using Application.Common.Models;
using Application.Debtss.Commands.CreateDebts;
using Application.Debtss.Commands.DeleteUser;
using Application.Debtss.Commands.UpdateUser;
using Application.Debtss.Queries.GetDebtsDetails;
using Application.Users.Queries.GetUserDetails;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers;
[Controller]
[Route("[controller]")]
public class DebtsController:BaseController
{
    private readonly IDebtService _debtService;
    public DebtsController(IMediator mediator, IMapper mapper, IDebtService debtService) : base(mediator, mapper)
    {
        _debtService = debtService;
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

        var vm = Mapper.Map<DebtDto>(await Mediator.Send(query));

        return Ok(vm);
    }
    
    [HttpGet]
    [Route("{email}")]
    public async Task<ActionResult<List<DebtDto>>> GetByUserEmail(string email)
    {
        var debtQuery = new GetDebtListQuery()
        {
            UserEmail = email
        };
        var debts = Mapper.Map<List<DebtDto>>(await Mediator.Send(debtQuery));

        var depositQuery = new GetDepositListQuery()
        {
            UserEmail = email
        };
        var deposits = await Mediator.Send(depositQuery);

        debts = _debtService.OfferTransferFromDepositToDebt(debts, deposits);

        return Ok(debts);
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

    [HttpPost]
    [Route("payments")]
    public async Task<ActionResult> AddPayment([FromBody] CreatePaymentCommand query)
    {
        var o = await Mediator.Send(query);
        return Ok();
    }
    
    
}