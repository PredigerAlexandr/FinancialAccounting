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
    public StatisticsController(IMediator mediator, IStatisticService statisticService) : base(mediator)
    {
        _statisticService = statisticService;
    }
    [Route("main-display-statistics")]
    public async Task<ActionResult<Object>> StatisticMainDisplay([FromBody] string userEmail)
    {
        var statistic = new StatisticMainPageVm()
        {
            TotalDebts = await _statisticService.GetTotalDebtsSum(userEmail),
            TotalSaves = await _statisticService.GetTotalDepositsSum(userEmail),
            TotalPayments = await _statisticService.GetTotalPaymentSum(userEmail),
            TotalPayoffs = await _statisticService.GetTotalPayoffSum(userEmail),
        };
        var lastStatistic = await _statisticService.
        return Ok(new());
    }
}