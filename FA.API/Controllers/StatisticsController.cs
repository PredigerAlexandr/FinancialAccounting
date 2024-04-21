using AutoMapper;
using CreditAPI.Models.Statistic;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    
    [Route("debts-statistics/{userEmail}")]
    [HttpGet]
    public async Task<ActionResult<decimal>> StatisticDebtsDisplay(string userEmail)
    {
        var totalPayoffs = await _statisticService.GetTotalPayoffSum(userEmail);
        
        return Ok(totalPayoffs);
    }
}