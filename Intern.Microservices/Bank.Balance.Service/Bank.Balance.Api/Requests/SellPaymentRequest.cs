namespace Bank.Balance.Api.Requests;

public sealed record SellPaymentRequest(
    string IdCard,
    string AccountNumber,
    string SecuritiesIdCard,
    string SecuritiesAccount,
    decimal Amount,
    string Description);
