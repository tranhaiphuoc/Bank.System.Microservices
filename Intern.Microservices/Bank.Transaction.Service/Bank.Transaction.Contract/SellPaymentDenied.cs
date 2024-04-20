namespace Bank.Transaction.Contract;

public sealed record SellPaymentDenied(string AccountNumber, string SecuritiesAccount, decimal Amount);
