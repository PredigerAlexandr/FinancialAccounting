using System.Runtime.InteropServices.JavaScript;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Jobs.JobService;

public class PaymentsService:IPaymentsService
{
    private readonly IServiceProvider _serviceProvider;

    public PaymentsService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task CheckDebtPayments()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
        var users = await dbContext.Users.Include(d => d.Debts)!.ThenInclude(d => d.Payments).ToListAsync();
        foreach (var user in users)
        {
            if (user.Debts.Count == 0)
            {
                continue;
            }

            foreach (var debt in user.Debts)
            {
                var currentDate = DateTime.Now;
                var lastPayment = debt.Payments.MinBy(p => p.DateCreate);
                if (currentDate.AddMonths(-1).Month != lastPayment.DateCreate.Month &&
                    currentDate.AddMonths(-1).Year != lastPayment.DateCreate.Year)
                {
                    dbContext.Notifications.Add(new Notification()
                    {
                        Date = DateTime.Now,
                        Description =
                            $"{user.Name}, Добрый день! В прошлом месяце вы не внесли плату по задолженности \"{debt.Name}\"",
                        Title = "Не внесена плата по задолженности",
                        Type = "Просрочена плата",
                        Url = $"/debts"
                    });
                }
            }
        }
        Console.WriteLine($"Джоба по оплате отработала {DateTime.Now.ToString()}");
        await dbContext.SaveChangesAsync(new CancellationToken());
    }
}