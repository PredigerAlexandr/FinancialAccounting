namespace Domain.Interfaces;

public interface IMoneySpendingService
{
    public Task<IList<string>> GetForecastingAsync(string userEmail);
}