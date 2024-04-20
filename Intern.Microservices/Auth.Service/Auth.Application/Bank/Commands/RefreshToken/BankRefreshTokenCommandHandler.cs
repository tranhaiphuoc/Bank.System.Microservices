using Auth.Application.Abstraction.Persistence;
using Auth.Application.Abstraction.Services;
using Auth.Domain.Enums;
using Auth.Domain.Shared;
using Auth.Domain.ValueObjects;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Auth.Domain.Errors;
using Mediator;

namespace Auth.Application.Bank.Commands.RefreshToken;

public class BankRefreshTokenCommandHandler(
    IAuthRepository authRepository,
    IJwtTokenService jwtTokenService,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<BankRefreshTokenCommand, Result<TokenPair>>
{
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async ValueTask<Result<TokenPair>> Handle(
        BankRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(
            ServiceAudience.Bank,
            request.AccessToken);

        if (principal is null)
        {
            return Result<TokenPair>.Failure(ErrorMessage.Unauthorized);
        }

        var jti = principal.Claims
           .SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

        if (jti is null)
        {
            return Result<TokenPair>.Failure(ErrorMessage.Unauthorized);
        }

        var username = principal.Claims.
            SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        if (username is null)
        {
            return Result<TokenPair>.Failure(ErrorMessage.Unauthorized);
        }

        var expUnix = long.Parse(principal.Claims
            .Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        var expUtc = _dateTimeProvider.CreateUtcDateTime(expUnix);
        var utcNow = _dateTimeProvider.UtcNow;

        if (expUtc > utcNow)
        {
            return Result<TokenPair>.Failure(ErrorMessage.TokenNotExpired);
        }

        var refreshToken = await _authRepository
            .GetRefreshToken(request.RefreshToken, cancellationToken);

        if (refreshToken is null)
        {
            return Result<TokenPair>.Failure(ErrorMessage.Unauthorized);
        }

        if (refreshToken.Token != request.RefreshToken ||
            refreshToken.JwtId.ToString() != jti ||
            refreshToken.ExpiredAt < utcNow ||
            refreshToken.IsRevoked is true ||
            refreshToken.UserId != username)
        {
            return Result<TokenPair>.Failure(ErrorMessage.Unauthorized);
        }

        var roleName = principal.Claims
            .SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (roleName is null)
        {
            return Result<TokenPair>.Failure(ErrorMessage.Unauthorized);
        }

        var result = _jwtTokenService.GenerateNewTokenPair(ServiceAudience.Bank, username, roleName);

        await _authRepository.RefreshAccessToken(
            request.RefreshToken,
            result.Token.RefreshToken,
            result.JwtId,
            result.RefreshTokenIssuedAt,
            result.RefreshTokenExpiredAt,
            username,
            cancellationToken);

        return Result<TokenPair>.Success(result.Token);
    }
}
