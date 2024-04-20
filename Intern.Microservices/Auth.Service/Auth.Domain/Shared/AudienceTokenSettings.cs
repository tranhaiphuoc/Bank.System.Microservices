using Auth.Configuration;
using Auth.Domain.ValueObjects;

namespace Auth.Domain.Shared;

public static class AudienceTokenSettings
{
    public static TokenSetting BankService { get; } = new(
        JwtParameters.Secret,
        JwtParameters.Issuer,
        JwtParameters.Audience.BankService,
        JwtParameters.ExpiryMinutes,
        JwtParameters.RefreshExpiryDays);
}
