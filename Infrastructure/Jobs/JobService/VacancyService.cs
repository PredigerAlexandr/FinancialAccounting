using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Infrastructure.Jobs.JobService;

public class VacancyService : IVacancyService
{
    private readonly IServiceProvider _services;

    public VacancyService(IServiceProvider services)
    {
        _services = services;
    }

    public async Task GetActualVacancy()
    {
        using (var scope = _services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<IDbContext>();
            var users = await dbContext.Users.Include(u => u.Notifications).ToListAsync();
            if (users.Count == 0)
            {
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                foreach (var user in users)
                {
                    if (user.City == null || user.Salary == null)
                    {
                        continue;
                    }

                    client.DefaultRequestHeaders.Add("User-Agent", "YourHeaderValue");
                    HttpResponseMessage response =
                        await client.GetAsync(
                            $"https://api.hh.ru/vacancies?text=\"{user.City}\"+AND+\"{user.ProfileWork}\"&period=10");
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var objects = JObject.Parse(responseBody);
                        foreach (var item in objects["items"])
                        {
                            if (item["salary"].HasValues && (int)item["salary"]["from"] > user.Salary &&
                                user.Notifications.Any(n => n.IdInMessage == (string)item["id"]) == false)
                            {
                                user.Notifications.Add(new Notification()
                                {
                                    Date = DateTime.Now,
                                    Description = (string)item["snippet"]["responsibility"]??"",
                                    IdInMessage = (string)item["id"],
                                    Title = (string)item["name"],
                                    Type = "Вакансия",
                                    Url = $"https://hh.ru/vacancy/{(string)item["id"]}",
                                    Salary = (int)item["salary"]["from"]
                                });
                            }
                        }

                        Console.WriteLine(responseBody);
                    }
                }

                Console.WriteLine($"Джоба по вакансиям отработала {DateTime.Now.ToString()}");
                await dbContext.SaveChangesAsync(new CancellationToken());
            }
        }
    }
}