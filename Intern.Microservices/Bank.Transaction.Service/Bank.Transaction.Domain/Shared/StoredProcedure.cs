namespace Bank.Transaction.Domain.Shared;

public static class StoredProcedure
{
    public static string GetApprovedTransaction { get; } = "getApprovedTransaction";
    public static string GetUnaprovedTransaction { get; } = "getUnaprovedTransaction";

    public static string GetDepositTransactionDetails { get; } = "getDepositTransactionDetails";
    public static string GetBuyPaymentTransactionDetails { get; } = "getBuyPaymentTransactionDetails";
    public static string GetSellPaymentTransactionDetails { get; } = "getSellPaymentTransactionDetails";


    public static string DepositMoneyTransaction { get; } = "depositMoneyTransaction";
    public static string DepositMoneyApproval { get; } = "depositMoneyApproval";

    public static string BuyPaymentTransaction { get; } = "buyPaymentTransaction";
    public static string BuyPaymentApproval { get; } = "buyPaymentApproval";

    public static string SellPaymentTransaction { get; } = "sellPaymentTransaction";
    public static string SellPaymentApproval { get; } = "sellPaymentApproval";
}
