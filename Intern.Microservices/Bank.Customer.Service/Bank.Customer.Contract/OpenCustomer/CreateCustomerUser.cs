namespace Bank.Customer.Contract.OpenCustomer;

public sealed record CreateCustomerUser(string IdCard, string PasswordHash, string AccountNumber);
