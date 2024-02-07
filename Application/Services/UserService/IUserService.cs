using Domain.Entities;

namespace Application.Services.UserService;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(string id);
}