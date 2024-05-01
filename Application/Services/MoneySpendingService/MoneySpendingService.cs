using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MathNet.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.MoneySpendingService;

public class MoneySpendingService : IMoneySpendingService
{
    private readonly IDbContext _dbContext;

    public MoneySpendingService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<string>> GetForecastingAsync(string userEmail)
    {
        var user = await _dbContext.Users.Where(u => u.Email == userEmail).Include(u => u.MoneySpendings)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            throw new NotFoundException(nameof(User), userEmail);
        }

        var types = Constant.TypeMoneySpendings;
        if (user?.IsAuto == true)
        {
            types = types.Concat(Constant.TypeMoneySpendingsForAuto).ToArray();
        }

        var forecastingList = new List<string>();
        var currentDate = DateTime.Now;

        foreach (var type in types)
        {
            var entites = user?.MoneySpendings.Where(m => m.Type == type)
                .OrderBy(m => m.Date).ToArray();
            if (entites != null && entites.Length > 2)
            {
                var nextEventDate = PredicateNextMoneyPayment(entites);
                if (nextEventDate > DateTime.Now
                    && (nextEventDate.Date.Year == currentDate.Date.Year
                        && nextEventDate.Date.Month == currentDate.Date.Month + 1))
                {
                    var avgAmount = entites.Average(m => m.Amount);
                    if (avgAmount >= user.Salary * 0.2)
                    {
                        forecastingList.Add(type);
                    }
                }
            }
        }

        return forecastingList;
    }


    private DateTime PredicateNextMoneyPayment(MoneySpending[] data)
    {
        double[] x = new double[data.Length];
        for (int i = 1; i < data.Length + 1; i++)
        {
            x[i - 1] = i;
        }

        double[] y = new double[data.Length];
        for (var i = 0; i < data.Length; i++)
        {
            y[i] = (data[i].Date - new DateTime(1970, 1, 1)).TotalDays;
        }

        // Построение модели с полиномиальной функцией
        Polynomial poly = Polynomial.Fit(x, y, 3);

        var nextEventIndex = data.Length + 1;
        double newY = poly.Evaluate(nextEventIndex);

        var nextEventDate = new DateTime(1970, 1, 1).AddDays(newY);

        return nextEventDate;
    }
}