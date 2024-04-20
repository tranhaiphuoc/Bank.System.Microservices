namespace Bank.Customer.Contract.OpenCustomer;

public sealed record CreateCustomerAccount(
    string IdCard,
    string AccountNumber,
    int TypeId);
