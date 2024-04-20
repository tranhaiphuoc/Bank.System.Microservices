using Auth.Application.Contracts;
using Auth.Domain.Shared;
using Mediator;

namespace Auth.Application.Bank.Queries.Login;

public sealed record BankLoginQuery(string Username, string Password) : IRequest<Result<LoginResult>>;
