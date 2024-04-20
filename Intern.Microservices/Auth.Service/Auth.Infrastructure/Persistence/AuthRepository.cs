using Auth.Application.Abstraction.Data;
using Auth.Application.Abstraction.Persistence;
using Auth.Domain.Enitties;
using Auth.Domain.Shared;
using Auth.Domain.ValueObjects;

namespace Auth.Infrastructure.Persistence;

public class AuthRepository(IDataAccess dataAccess) : IAuthRepository
{
    private readonly IDataAccess _dataAccess = dataAccess;

    public async Task<UserCredential?> GetUser(
        string username,
        CancellationToken cancellationToken = default)
    {
        var parameters = new { username };

        var result = await _dataAccess.LoadDataAsync<UserCredential, dynamic>(
            StoredProcedure.GetUser,
            parameters,
            cancellationToken: cancellationToken);

        return result.FirstOrDefault();
    }

    public async Task<RefreshToken?> GetRefreshToken(
        string token,
        CancellationToken cancellationToken = default)
    {
        var parameters = new { token };

        var result = await _dataAccess.LoadDataAsync<RefreshToken, dynamic>(
            StoredProcedure.GetRefreshToken,
            parameters,
            cancellationToken: cancellationToken);

        return result.FirstOrDefault();
    }

    public async Task<AffectedRow> AddUser(
        string username,
        string passwordHash,
        string accountNumber,
        CancellationToken cancellationToken = default)
    {
        var parameters = new { username, passwordHash, accountNumber };

        var rows = await _dataAccess.SaveDataAsync(
            StoredProcedure.AddUser,
            parameters,
            cancellationToken: cancellationToken);

        return new(rows);
    }

    public async Task<AffectedRow> AddRefreshToken(
        string token,
        string jwtId,
        DateTime issuedAt,
        DateTime expiredAt,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var parameters = new { token, jwtId, issuedAt, expiredAt, userId };

        var rows = await _dataAccess.SaveDataAsync(
            StoredProcedure.AddRefreshToken,
            parameters,
            cancellationToken: cancellationToken);

        return new(rows);
    }

    public async Task<ErrorCode> RefreshAccessToken(
        string expiredToken,
        string newToken,
        string jwtId,
        DateTime issuedAt,
        DateTime expiredAt,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var parameters = new
       {
            expiredToken,
            newToken,
            jwtId,
            issuedAt,
            expiredAt,
            userId
        };

        var errorCode = await _dataAccess.LoadDataAsync<int, dynamic>(
            StoredProcedure.RefreshAccessToken,
            parameters,
            cancellationToken: cancellationToken);

        return new(errorCode.First());
    }

    public async Task<AffectedRow> RevokeRefreshToken(
        string token,
        CancellationToken cancellationToken = default)
    {
        var parameters = new { token };

        var rows = await _dataAccess.SaveDataAsync(
            StoredProcedure.RevokeRefreshToken,
            parameters,
            cancellationToken: cancellationToken);

        return new(rows);
    }
}
