using Auth.Application.Abstraction.Services;
using Auth.Domain.Enums;
using Auth.Configuration;
using Microsoft.IdentityModel.Tokens;
using Auth.Domain.ValueObjects;
using Auth.Domain.Shared;

namespace Auth.Infrastructure.Services;

public class JwtSettings : IJwtSettings
{
    public TokenSetting GetJwtSettings(ServiceAudience serviceAudience)
    {
        var setting = serviceAudience switch
        {
            ServiceAudience.Bank => AudienceTokenSettings.BankService,
            _ => AudienceTokenSettings.BankService
        };

        return setting;
    }

    public TokenValidationParameters GetTokenValidationParameters(
        ServiceAudience audiences,
        bool validateLifetime = true)
    {
        var tokenValidationParameters = audiences switch
        {
            ServiceAudience.Bank => JwtParameters
                .ValidationParametersForBankService(validateLifetime),

            _ => JwtParameters
                .ValidationParametersForBankService(validateLifetime)
        };

        return tokenValidationParameters;
    }
}
