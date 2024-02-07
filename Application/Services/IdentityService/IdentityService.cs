using System.Security.Cryptography;
using System.Text;
using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.IdentityService;

/// <summary>
/// Сервис по работе с аутентификацией и генерацией соли
/// </summary>
public class IdentityService : IIdentityService
{
    private readonly IDbContext _context;

    public IdentityService(IDbContext context)
    {
        _context = context;
    }

    public async Task<User?> LoginUserAsync(UserLoginDto userLoginDto, string secretKey)
    {
        var hashPassword = GetMD5Hash(userLoginDto.Password + secretKey);
        var user = await _context.Users.Where(u => u.Email == userLoginDto.Email)
            .Where(u => u.Password == hashPassword).FirstOrDefaultAsync();
        return user;
    }

    public string GetMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString().Substring(0, 16);
        }
    }
}