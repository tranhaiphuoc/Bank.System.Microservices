using Bank.Balance.Domain.Shared;
using Bank.Balance.Domain.ValueObjects;
using Mediator;

namespace Bank.Balance.Application.Commands.SellPayment;

public sealed record SellPaymentCommand(
    string IdCard,
    string AccountNumber,
    string SecuritiesIdCard,
    string SecuritiesAccount,
    decimal Amount,
    string Description) : IRequest<Result<AccountBalance>>;
