using Auth.Application.Abstraction.Persistence;
using Auth.Application.Abstraction.Services;
using Auth.Application.Contracts;
using Auth.Domain.Enums;
using Auth.Domain.Errors;
using Auth.Domain.Shared;
using Mediator;

namespace Auth.Application.Bank.Queries.Login;

public class BankLoginQueryHandler(
    IHelper helper,
    IJwtTokenService jwtTokenService,
    IAuthRepository authRepository) : IRequestHandler<BankLoginQuery, Result<LoginResult>>
{
    private readonly IHelper _helper = helper;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IAuthRepository _authRepository = authRepository;

    public async ValueTask<Result<LoginResult>> Handle(
        BankLoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _authRepository.GetUser(request.Username, cancellationToken);
        
        if (user is null)
        {
            return Result<LoginResult>.Failure(ErrorMessage.InvalidCredential);
        }

        var isPasswordValid = _helper
            .VerifyPasswordHash(request.Password, user.PasswordHash);

        if (isPasswordValid is false)
        {
            return Result<LoginResult>.Failure(ErrorMessage.InvalidCredential);
        }

        var result = _jwtTokenService
            .GenerateNewTokenPair(ServiceAudience.Bank, user.Username, user.RoleName);

        await _authRepository.AddRefreshToken(
            result.Token.RefreshToken,
            result.JwtId,
            result.RefreshTokenIssuedAt,
            result.RefreshTokenExpiredAt,
            user.Username,
            cancellationToken);

        return Result<LoginResult>.Success(new(user.AccountNumber, user.Username, result.Token));
    }
}
