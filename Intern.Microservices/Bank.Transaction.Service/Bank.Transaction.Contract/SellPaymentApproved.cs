namespace Bank.Transaction.Contract;

public sealed record SellPaymentApproved(string AccountNumber, string SecuritiesAccount, decimal Amount);
