namespace Auth.Api.Contracts.Requests.Bank;

public sealed record BankRegisterRequest(
    int AccountType,
    string AccountNo,
    string IdCard,
    DateTime CardDate,
    string CardPlace,
    string Name,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Address,
    string Password);
