using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PostgreSqlDbContext : DbContext, IDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }


    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new LoanConfiguration());
        base.OnModelCreating(builder);
    }
}