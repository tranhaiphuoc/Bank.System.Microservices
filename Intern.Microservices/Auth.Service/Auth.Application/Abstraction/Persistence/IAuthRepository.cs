using Auth.Domain.Enitties;
using Auth.Domain.ValueObjects;

namespace Auth.Application.Abstraction.Persistence;

public interface IAuthRepository
{
    Task<UserCredential?> GetUser(
        string username,
        CancellationToken cancellationToken = default);

    Task<RefreshToken?> GetRefreshToken(
        string token,
        CancellationToken cancellationToken = default);

    Task<AffectedRow> AddUser(
        string username,
        string passwordHash,
        string acountNumber,
        CancellationToken cancellationToken = default);

    Task<AffectedRow> AddRefreshToken(
        string token,
        string jwtId,
        DateTime issuedAt,
        DateTime expiredAt,
        string userId,
        CancellationToken cancellationToken = default);

    Task<ErrorCode> RefreshAccessToken(
        string expiredToken,
        string newToken,
        string jwtId,
        DateTime issuedAt,
        DateTime expiredAt,
        string userId,
        CancellationToken cancellationToken = default);

    Task<AffectedRow> RevokeRefreshToken(
        string token,
        CancellationToken cancellationToken = default);
}
