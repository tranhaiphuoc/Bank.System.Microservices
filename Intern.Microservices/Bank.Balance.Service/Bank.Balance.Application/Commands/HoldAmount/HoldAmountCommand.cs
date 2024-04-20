using Bank.Balance.Domain.Shared;
using Mediator;

namespace Bank.Balance.Application.Commands.HoldAmount;

public sealed record HoldAmountCommand(
    string IdCard,
    string AccountNumber,
    decimal Amount,
    string Description) : IRequest<Result>;
