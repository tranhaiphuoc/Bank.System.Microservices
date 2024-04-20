using Auth.Application.Abstraction.Persistence;
using Auth.Application.Abstraction.Services;
using Auth.Domain.Enums;
using Auth.Domain.Errors;
using Auth.Domain.Shared;
using Mediator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Application.Bank.Commands.RevokeRefreshToken;

public class BankRevokeRefreshTokenCommandHandler(
    IJwtTokenService jwtTokenService,
    IAuthRepository authRepository) : IRequestHandler<BankRevokeRefreshTokenCommand, Result>
{
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IAuthRepository _authRepository = authRepository;

    public async ValueTask<Result> Handle(
        BankRevokeRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(
            ServiceAudience.Bank,
            request.AccessToken);

        if (principal is null)
        {
            return Result.Failure(ErrorMessage.Unauthorized);
        }

        var username = principal.Claims
           .SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        if (username is null)
        {
            return Result.Failure(ErrorMessage.Unauthorized);
        }

        var jti = principal.Claims
            .SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

        var refreshToken = await _authRepository
            .GetRefreshToken(request.RefreshToken, cancellationToken);

        if (refreshToken is null ||
            refreshToken.JwtId.ToString() != jti ||
            refreshToken.UserId != username)
        {
            return Result.Failure(ErrorMessage.Unauthorized);
        }

        await _authRepository.RevokeRefreshToken(request.RefreshToken, cancellationToken);

        return Result.Success();
    }
}
