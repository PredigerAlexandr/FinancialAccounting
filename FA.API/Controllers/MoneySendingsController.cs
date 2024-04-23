using System.Globalization;
using System.Reflection.Metadata;
using Application.CommandsAndQueries.Debts.Commands.CreateDebt;
using Application.CommandsAndQueries.Debts.Queries.GetDebtListQuery;
using Application.CommandsAndQueries.MoneySpendings.Commands.DeleteMoneySpending;
using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;
using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListQuery;
using Application.CommandsAndQueries.Users.Queries.GetUserDetails;
using Application.Debtss.Commands.DeleteUser;
using Application.Debtss.Queries.GetDebtsDetails;
using AutoMapper;
using CreditAPI.Models.Statistic;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Constants = Application.Common.Constants;

namespace CreditAPI.Controllers;

[Controller]
[Route("[controller]")]
public class MoneySpendingsController : BaseController
{
    public MoneySpendingsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    [Route("list/{userEmail}")]
    public async Task<ActionResult<IList<MoneySpending>>?> GetList(string userEmail)
    {
        var query = new GetMoneySpendingListQuery()
        {
            UserEmail = userEmail
        };
        var entities = await Mediator.Send(query);
        return Ok( entities);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<MoneySpending>> Get(string id)
    {
        var query = new GetMoneySpendingDetailsQuery()
        {
            Id = new Guid(id)
        };
        var entities = await Mediator.Send(query);
        return Ok(entities);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var query = new DeleteMoneySpendingCommand()
        {
            Id = new Guid(id)
        };
        var entities = await Mediator.Send(query);
        return Ok(entities);
    }
    
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CreateMoneySpendingCommand query)
    {
        await Mediator.Send(query);
        return Ok();
    }
    
    [HttpGet]
    [Route("types/{userEmail}")]
    public async Task<ActionResult<string[]>> GetTypes(string userEmail)
    {
        var query = new GetUserDetailsQuery()
        {
            Email = userEmail
        };
        var user = await Mediator.Send(query);

        return Ok(user.IsAuto is true
            ? Constants.Constant.TypeMoneySpendingsForAuto.Concat(Constants.Constant.TypeMoneySpendings)
            : Constants.Constant.TypeMoneySpendings);
    }
    
    [HttpGet]
    [Route("money-spendings-per-months/{userEmail}")]
    public async Task<ActionResult<IList<SeriesElementVm<string,int>>>> GetArray(string userEmail)
    {
        var currentTime = DateTime.Today;
        var startCalculateDate = currentTime.AddMonths(-12);
        var seriesObjects = new List<SeriesElementVm<string, int>>();
        var queryUser = new GetUserDetailsQuery()
        {
            Email = userEmail
        };
        var user = await Mediator.Send(queryUser);
        var jkhSummer = user.JkhSummer ?? 0;
        var jkhWinter = user.JkhWinter ?? 0;
        var anotherPayment = user.AnotherPayments ?? 0;

        for (int i = 0; i < 12; i++)
        {
            var date = startCalculateDate.AddMonths(i);
            var queryMoney = new GetMoneySpendingListByMonthQuery()
            {
                UserEmail = userEmail,
                Date = date
            };
            var entities = await Mediator.Send(queryMoney);

            var seriesObject = new SeriesElementVm<string, int>()
            {
                Name = Constants.Constant.Months[date.Month],
                Value = anotherPayment
            };

            if (date.Month is >= 4 and <= 10)
            {
                seriesObject.Value += jkhSummer;
            }
            else
            {
                seriesObject.Value += jkhWinter;
            }
            
            seriesObjects.Add(seriesObject);
        }

        return Ok(seriesObjects);
    }
    
}