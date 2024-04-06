using Application.CommandsAndQueries.Payoffs.Queries.GetPayoffListQuery;
using Application.Interfaces;
using Application.Payments.Queries.GetDepositListQuery;
using Application.Users.Queries.GetUserDetails;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Services.StatisticService;

public class StatisticService:ServiceBase, IStatisticService
{
    private readonly IDebtService _debtService;

    public StatisticService(IMediator mediator, IDebtService debtService):base(mediator)
    {
        _debtService = debtService;
    }

    public async Task<decimal> GetTotalDebtsSum(string userEmail)
    {
        var entities = await Mediator.Send(new GetDebtListQuery() { UserEmail = userEmail });
        
        if (entities == null)
            return 0;
        
        var totalResult = entities.Sum(e => e.CurrentSum);

        return totalResult;
    }

    public async Task<decimal> GetTotalDepositsSum(string userEmail){
        var entities = await Mediator.Send(new GetDepositListQuery() { UserEmail = userEmail });
        
        if (entities == null)
            return 0;
        
        var totalResult = entities.Sum(e => e.CurrentSum);

        return totalResult;
    }
    
    
    public async Task<decimal> GetTotalPaymentSum(string userEmail){
        var entities = await Mediator.Send(new GetPaymentListQuery(){ UserEmail = userEmail });
        var totalResult = entities.Sum(e => e.Amount);

        return totalResult;
    }
    
    public async Task<decimal> GetTotalPayoffSum(string userEmail){
        var entities = await Mediator.Send(new GetPayoffListQuery(){ UserEmail = userEmail });
        var totalResult = entities.Sum(e => e.Amount);

        return totalResult;
    }
    
    public async Task<Statistic> GetLastStatistic(string userEmail)
    {
        
    }
}