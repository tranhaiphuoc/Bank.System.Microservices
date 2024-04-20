using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.Configuration;

public static class JwtParameters
{
    public static class Audience
    {
        public static string BankService { get; } = "Bank.Service";
    }


    public static string Secret { get; } = "my-super-duper-secret-key-blah-blah-blah";
    public static string Issuer { get; } = "Auth.Service";
    public static int ExpiryMinutes { get; } = 15;
    public static int RefreshExpiryDays { get; } = 30;

    
    public static TokenValidationParameters ValidationParametersForBankService(
        bool validateLifetime = true)
    {
        return new TokenValidationParameters
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience.BankService,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = validateLifetime
        };
    }
}
