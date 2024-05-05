using System.Formats.Asn1;
using Hangfire;
using Infrastructure.Jobs.JobService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Jobs;

public class VacancySearchJob: BackgroundService
{
    private readonly IServiceProvider _services;
    private IRecurringJobManager _recurringJobManager;
    public VacancySearchJob(IRecurringJobManager recurringJobManager, IServiceProvider services)
    {
        _recurringJobManager = recurringJobManager;
        _services = services;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _services.CreateScope())
        {
            var vacancyService = scope.ServiceProvider.GetService<IVacancyService>();
            _recurringJobManager.AddOrUpdate(
                "GetActualVacancyJob",
                () => vacancyService.GetActualVacancy(),
                Cron.Minutely);
        }
    }
}