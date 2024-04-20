using Auth.Application.Abstraction.Services;
using Auth.Application.Contracts;
using Auth.Domain.Enums;
using Auth.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Infrastructure.Services;

public class JwtTokenService(
    IDateTimeProvider dateTimeProvider,
    IJwtSettings jwtSettings) : IJwtTokenService
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IJwtSettings _jwtSettings = jwtSettings;

    public GenerateTokenResult GenerateNewTokenPair(
        ServiceAudience serviceAudience,
        string username,
        string roleName)
    {
        var tokenSettings = _jwtSettings.GetJwtSettings(serviceAudience);

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, roleName)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var utcNow = _dateTimeProvider.UtcNow;

        var jwtToken = new JwtSecurityToken(
            issuer: tokenSettings.Issuer,
            audience: tokenSettings.Audience,
            expires: utcNow.AddSeconds(tokenSettings.AccessExpiryMinutes + 15),
            claims: claims,
            signingCredentials: signingCredentials);

        var refreshToken = GenerateRefreshToken();

        var tokenPair = new TokenPair(
            new JwtSecurityTokenHandler().WriteToken(jwtToken),
            jwtToken.ValidTo,
            refreshToken);

        return new GenerateTokenResult(
            jwtToken.Id,
            utcNow,
            utcNow.AddDays(tokenSettings.RefreshExpiryDays),
            tokenPair);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(
        ServiceAudience serviceAudience,
        string accessToken)
    {
        try
        {
            var validationParams = _jwtSettings
           .GetTokenValidationParameters(serviceAudience, false);

            var claims = new JwtSecurityTokenHandler()
                .ValidateToken(accessToken, validationParams, out var validatedToken);

            if (validatedToken is null)
            {
                return null;
            }

            return claims;
        }
        catch
        {
            return null;
        }
    }

    public bool IsJwtTokenFormatCorrect(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken securityToken) && 
            securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }

    private static string GenerateRefreshToken()
    {
        byte[] random = new byte[64];

        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(random);

        return Convert.ToBase64String(random);
    }
}
