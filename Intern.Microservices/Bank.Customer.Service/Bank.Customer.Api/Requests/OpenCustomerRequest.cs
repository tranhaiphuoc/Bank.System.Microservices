namespace Bank.Customer.Api.Requests;

public sealed record OpenCustomerRequest(
    string IdCard,
    string Name,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Address,
    DateTime IdCardDate,
    string IdCardPlace,
    string AccountNumber,
    int AccountType,
    string Password);
