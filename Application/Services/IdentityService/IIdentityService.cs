using Application.Common.Models;
using Domain.Entities;

namespace Application.Services.IdentityService;

public interface IIdentityService
{
    Task<User?> LoginUserAsync(UserLoginDto userLoginDto, string secretKey);
    string GetMD5Hash(string input);
}