using Application.CommandsAndQueries.MoneySpendings.Queries.GetMoneySpendingListByMonthQuery;
using Application.CommandsAndQueries.Payments.Queries.GetPaymentsPerMonth;
using Application.CommandsAndQueries.Payoffs.Queries.GetPayoffsPerMonth;
using Application.CommandsAndQueries.Users.Queries.GetUserDetails;
using AutoMapper;
using CreditAPI.Models.Statistic;
using Domain.Interfaces;
using MathNet.Numerics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Constants = Application.Common.Constants;

namespace CreditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StatisticsController : BaseController
{
    private readonly IStatisticService _statisticService;
    public StatisticsController(IMediator mediator, IStatisticService statisticService, IMapper mapper) : base(mediator, mapper)
    {
        _statisticService = statisticService;
    }
    [Route("main-display-statistics/{userEmail}")]
    [HttpGet]
    public async Task<ActionResult<StatisticMainPageVm>> StatisticMainDisplay(string userEmail)
    {
        var statistic = new StatisticMainPageVm()
        {
            TotalDebts = await _statisticService.GetTotalDebtsSum(userEmail),
            TotalDeposits = await _statisticService.GetTotalDepositsSum(userEmail),
            TotalPayments = await _statisticService.GetTotalPaymentSum(userEmail),
            TotalPayoffs = await _statisticService.GetTotalPayoffSum(userEmail),
        };
        
        var lastStatistic = await _statisticService.GetLastStatisticAsync(userEmail);
        
        if (lastStatistic != null)
        {
            statistic.TotalDebtsDelta =
                _statisticService.CalculateDelta(lastStatistic.TotalDebts, statistic.TotalDebts);
            statistic.TotalDepositsDelta =
                _statisticService.CalculateDelta(lastStatistic.TotalDeposits, statistic.TotalDeposits);
            statistic.TotalPaymentsDelta =
                _statisticService.CalculateDelta(lastStatistic.TotalPayments, statistic.TotalPayments);
            statistic.TotalPayoffsDelta =
                _statisticService.CalculateDelta(lastStatistic.TotalPayoffs, statistic.TotalPayoffs);
        }
        
        return Ok(statistic);
    }
    
    [Route("deposit-statistics/{userEmail}")]
    [HttpGet]
    public async Task<ActionResult<decimal>> StatisticDepositDisplay(string userEmail)
    {
        var totalPayoffs = await _statisticService.GetTotalPayoffSum(userEmail);
        
        return Ok(totalPayoffs);
    }
    
    [HttpGet]
    [Route("payoffs-per-months/{userEmail}")]
    public async Task<ActionResult<IList<SeriesElementVm<string,int>>>> GetTotalPayoffsPerMonths(string userEmail)
    {
        var currentTime = DateTime.Today;
        var startCalculateDate = currentTime.AddMonths(-12);
        var seriesObjects = new List<SeriesElementVm<string, int>>();
        var queryUser = new GetUserDetailsQuery()
        {
            Email = userEmail
        };
        var user = await Mediator.Send(queryUser);

        for (int i = 0; i < 12; i++)
        {
            var date = startCalculateDate.AddMonths(i);
            var queryMoney = new GetPayoffsPerMonthQuery()
            {
                UserEmail = userEmail,
                Date = date
            };
            var entities = await Mediator.Send(queryMoney);

            var seriesObject = new SeriesElementVm<string, int>()
            {
                Name = Constants.Constant.Months[date.Month],
                Value = (int)(entities?.Sum(e=>e.Amount) ?? 0)
            };
            
            seriesObjects.Add(seriesObject);
        }

        return Ok(seriesObjects);
    }
    
    [HttpGet]
    [Route("payments-per-months/{userEmail}")]
    public async Task<ActionResult<IList<SeriesElementVm<string,int>>>> GetTotalPaymentsPerMonths(string userEmail)
    {
        var currentTime = DateTime.Today;
        var startCalculateDate = currentTime.AddMonths(-12);
        var seriesObjects = new List<SeriesElementVm<string, int>>();
        var queryUser = new GetUserDetailsQuery()
        {
            Email = userEmail
        };
        var user = await Mediator.Send(queryUser);

        for (int i = 0; i < 12; i++)
        {
            var date = startCalculateDate.AddMonths(i);
            var queryMoney = new GetPaymentsPerMonthQuery()
            {
                UserEmail = userEmail,
                Date = date
            };
            var entities = await Mediator.Send(queryMoney);

            var seriesObject = new SeriesElementVm<string, int>()
            {
                Name = Constants.Constant.Months[date.Month],
                Value = (int)(entities?.Sum(e=>e.Amount) ?? 0)
            };
            
            seriesObjects.Add(seriesObject);
        }

        return Ok(seriesObjects);
    }
    
    
    [Route("debts-statistics/{userEmail}")]
    [HttpGet]
    public async Task<ActionResult<decimal>> StatisticDebtsDisplay(string userEmail)
    {
        var totalPayoffs = await _statisticService.GetTotalPayoffSum(userEmail);
        
        return Ok(totalPayoffs);
    }
}