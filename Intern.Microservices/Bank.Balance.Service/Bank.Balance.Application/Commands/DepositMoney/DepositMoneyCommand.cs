using Bank.Balance.Domain.Shared;
using Mediator;

namespace Bank.Balance.Application.Commands.DepositMoney;

public sealed record DepositMoneyCommand(
    string AccountNumber,
    decimal Amount,
    string Description) : IRequest<Result>;
