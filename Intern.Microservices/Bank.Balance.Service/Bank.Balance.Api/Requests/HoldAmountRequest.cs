namespace Bank.Balance.Api.Requests;

public sealed record HoldAmountRequest(
    string IdCard,
    string AccountNumber,
    decimal Amount,
    string Description);
