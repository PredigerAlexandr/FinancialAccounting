namespace Application.Common.Models;

public record UserLoginDto
{
    public string Email { get; init; }
    public string Password { get; set; }
}