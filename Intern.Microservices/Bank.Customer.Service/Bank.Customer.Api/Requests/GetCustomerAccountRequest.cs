namespace Bank.Customer.Api.Requests;

public sealed record GetCustomerAccountRequest(string AccountNumber, string IdCard);
