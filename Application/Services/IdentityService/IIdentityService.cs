namespace Application.Services.IdentityService;

public interface IIdentityService
{
    public string Challenge { get; }
    public string GenerateChallenge();
}