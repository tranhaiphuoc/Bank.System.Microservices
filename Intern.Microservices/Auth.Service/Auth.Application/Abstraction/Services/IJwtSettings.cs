using Auth.Domain.Enums;
using Auth.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Application.Abstraction.Services;

public interface IJwtSettings
{
    TokenSetting GetJwtSettings(ServiceAudience serviceAudience);

    TokenValidationParameters GetTokenValidationParameters(
        ServiceAudience audiences, bool validateLifetime = true);
}
