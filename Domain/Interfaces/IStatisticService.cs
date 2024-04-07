using Domain.Entities;

namespace Domain.Interfaces;

public interface IStatisticService
{
    public Task<decimal> GetTotalDebtsSum(string userEmail);
    public Task<decimal> GetTotalDepositsSum(string userEmail);
    public Task<decimal> GetTotalPaymentSum(string userEmail);
    public Task<decimal> GetTotalPayoffSum(string userEmail);
    public Task<Statistic?> GetLastStatisticAsync(string userEmail);
    public int CalculateDelta(decimal lastValue, decimal currentValue);
}