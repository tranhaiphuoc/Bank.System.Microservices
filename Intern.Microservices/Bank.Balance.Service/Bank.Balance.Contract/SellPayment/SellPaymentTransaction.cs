namespace Bank.Balance.Contract.SellPayment;

public sealed record SellPaymentTransaction(
    string AccountNumber,
    string SecuritiesAccount,
    decimal Amount,
    string Description);
