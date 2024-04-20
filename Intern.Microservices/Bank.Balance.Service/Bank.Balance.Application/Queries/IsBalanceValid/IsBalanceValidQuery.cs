using Bank.Balance.Domain.Shared;
using Mediator;

namespace Bank.Balance.Application.Queries.IsBalanceValid;

public sealed record IsBalanceValidQuery(
    string IdCard,
    string AccountNumber) : IRequest<Result>;
