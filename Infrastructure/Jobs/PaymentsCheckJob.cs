using Hangfire;
using Infrastructure.Jobs.JobService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Jobs;

public class PaymentsCheckJob : BackgroundService
{
    private readonly IServiceProvider _services;
    private IRecurringJobManager _recurringJobManager;

    public PaymentsCheckJob(IRecurringJobManager recurringJobManager, IServiceProvider services)
    {
        _recurringJobManager = recurringJobManager;
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _services.CreateScope())
        {
            var paymentsService = scope.ServiceProvider.GetRequiredService<IPaymentsService>();
            _recurringJobManager.AddOrUpdate(
                "DebtPaymentsCheckJob",
                () => paymentsService.CheckDebtPayments(),
                Cron.Minutely);
        }
    }
}