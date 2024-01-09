using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Loan> Loans { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}