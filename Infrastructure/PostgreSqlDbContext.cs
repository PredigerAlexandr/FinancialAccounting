using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PostgreSqlDbContext : DbContext, IDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Debt> Debts { get; set; }
    public DbSet<BankDeposit> BankDeposits { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Payoff> Payoffs { get; set; }
    public DbSet<Statistic> Statistics { get; set; }


    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new DebtsConfiguration());
        base.OnModelCreating(builder);
    }
}