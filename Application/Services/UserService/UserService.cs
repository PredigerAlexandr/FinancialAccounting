using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.UserService;

public class UserService
{
    private readonly IDbContext _context;
    public UserService(IDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.Where(u => u.Id == new Guid(email)).FirstOrDefaultAsync();
    }
}