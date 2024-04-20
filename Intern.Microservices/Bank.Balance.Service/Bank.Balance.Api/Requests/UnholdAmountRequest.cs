namespace Bank.Balance.Api.Requests;

public sealed record UnholdAmountRequest(
    string IdCard,
    string AccountNumber,
    decimal Amount,
    string Description);
