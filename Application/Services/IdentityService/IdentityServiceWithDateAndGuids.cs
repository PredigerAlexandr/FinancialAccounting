using System.Globalization;
using Application.Services.IdentityService;

namespace Application.Services;
/// <summary>
/// Сервис по работе с аутентификацией и генерацией соли
/// </summary>
public class IdentityServiceWithDateAndGuids:IIdentityService
{
    private string? _challenge;

    public string Challenge => _challenge ?? GenerateChallenge();
    /// <summary>
    /// Метод генерации соли, отправляемой на клиент
    /// </summary>
    /// <returns>соль из 2 гуидов и даты</returns>
    public string GenerateChallenge()
    {
        _challenge = Guid.NewGuid().ToString() + DateTime.Now.ToString() + Guid.NewGuid().ToString();
        return _challenge;
    }
}