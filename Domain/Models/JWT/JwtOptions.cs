namespace Domain.Models.JWT;

public class JwtOptions
{
    public const string ISSUER = "http://localhost:5218/"; // издатель токена
    public const string AUDIENCE = "http://localhost:5218/"; // потребитель токена
    public const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
}