using Auth.Domain.Shared;
using Auth.Domain.ValueObjects;
using Mediator;

namespace Auth.Application.Bank.Commands.RefreshToken;

public sealed record BankRefreshTokenCommand(
    string AccessToken,
    string RefreshToken) : IRequest<Result<TokenPair>>;
