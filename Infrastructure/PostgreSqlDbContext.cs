﻿using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class PostgreSqlDbContext : DbContext, IDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<UserAccess> UserAccesses { get; set; }

    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserAccessConfiguration());
        builder.ApplyConfiguration(new LoanConfiguration());
        base.OnModelCreating(builder);
    }
}