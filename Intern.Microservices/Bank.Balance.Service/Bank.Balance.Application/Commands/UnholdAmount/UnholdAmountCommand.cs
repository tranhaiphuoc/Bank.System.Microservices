using Bank.Balance.Domain.Shared;
using Mediator;

namespace Bank.Balance.Application.Commands.UnholdAmount;

public sealed record UnholdAmountCommand(
    string IdCard,
    string AccountNumber,
    decimal Amount,
    string Description) : IRequest<Result>;