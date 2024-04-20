namespace Bank.Balance.Contract.BuyPayment;

public sealed record BuyPaymentTransaction(
    string AccountNumber,
    string SecuritiesAccount,
    decimal Amount,
    string Description);
