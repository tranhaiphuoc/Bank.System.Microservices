using Auth.Application.Contracts;
using Auth.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Auth.Application.Abstraction.Services;

public interface IJwtTokenService
{
    GenerateTokenResult GenerateNewTokenPair(
        ServiceAudience serviceAudience,
        string username,
        string roleName);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(
        ServiceAudience serviceAudience,
        string token);

    bool IsJwtTokenFormatCorrect(SecurityToken validatedToken);
}
