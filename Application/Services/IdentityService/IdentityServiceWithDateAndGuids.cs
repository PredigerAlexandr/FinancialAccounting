using System.Globalization;
using Application.Services.IdentityService;

namespace Application.Services;

public class IdentityServiceWithDateAndGuids:IIdentityService
{
    private string? _challenge;

    public string Challenge => _challenge ?? GenerateChallenge();

    public string GenerateChallenge()
    {
        _challenge = Guid.NewGuid().ToString() + DateTime.Now.ToString() + Guid.NewGuid().ToString();
        return _challenge;
    }
}