namespace Bank.Customer.Domain.ValueObjects;

public record CustomerAccount(
    string AccountNumber,
    string Name,
    DateTime DateOfBirth,
    string IdCard,
    DateTime IdCardDate,
    string IdCardPlace);
