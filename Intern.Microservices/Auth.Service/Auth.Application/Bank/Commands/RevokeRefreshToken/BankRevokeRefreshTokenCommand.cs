using Auth.Domain.Shared;
using Mediator;

namespace Auth.Application.Bank.Commands.RevokeRefreshToken;

public sealed record BankRevokeRefreshTokenCommand(
    string AccessToken,
    string RefreshToken) : IRequest<Result>;
