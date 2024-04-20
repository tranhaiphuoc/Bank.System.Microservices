using Bank.Balance.Domain.Shared;
using Bank.Balance.Domain.ValueObjects;
using Mediator;

namespace Bank.Balance.Application.Queries.GetBalance;

public sealed record GetBalanceQuery(
    string IdCard,
    string AccountNumber) : IRequest<Result<AccountBalance>>;
