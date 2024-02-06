namespace Application.Services.IdentityService;

public interface IIdentityService
{
    public string GetMD5Hash(string input);
}