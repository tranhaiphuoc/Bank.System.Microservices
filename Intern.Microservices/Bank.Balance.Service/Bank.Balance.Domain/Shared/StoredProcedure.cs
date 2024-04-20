namespace Bank.Balance.Domain.Shared;

public static class StoredProcedure
{
    public static string IsBalanceExist { get; } = "isBalanceExist";
    public static string IsBalanceValid { get; } = "isBalanceValid";
    public static string GetBalance { get; } = "getBalance";
    public static string AddBalance { get; } = "addBalance";
    public static string HoldAmount { get; } = "holdAmount";
    public static string UnholdAmount { get; } = "unholdAmount";
    public static string BuyPayment { get; } = "buyPayment";
    public static string SellPayment { get; } = "sellPayment";
    public static string DepositMoneyApproved { get; } = "depositMoneyApproved";
    public static string BuyPaymentApproved { get; } = "buyPaymentApproved";
    public static string BuyPaymentDenied { get; } = "buyPaymentDenied";
    public static string SellPaymentApproved { get; } = "sellPaymentApproved";
    public static string SellPaymentDenied { get; } = "sellPaymentDenied";
}
