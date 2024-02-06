using System.Globalization;
using Application.Services.IdentityService;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services;
/// <summary>
/// Сервис по работе с аутентификацией и генерацией соли
/// </summary>
public class IdentityServiceWithDateAndGuids:IIdentityService
{
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