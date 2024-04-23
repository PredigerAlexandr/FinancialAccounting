using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Debt> Debts { get; set; }
    DbSet<BankDeposit> BankDeposits { get; set; }
    DbSet<Payment> Payments { get; set; }
    public DbSet<Payoff> Payoffs { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<MoneySpending> MoneySpendings { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}