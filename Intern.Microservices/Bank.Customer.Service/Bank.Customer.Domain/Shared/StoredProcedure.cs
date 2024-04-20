namespace Bank.Customer.Domain.Shared;

public static class StoredProcedure
{
    public static string IsCustomerAccountValid { get; } = "isCustomerAccountValid";
    public static string GetCustomerAccount { get; } = "getCustomerAccount";
    public static string OpenCustomer { get; } = "openCustomer";
}
