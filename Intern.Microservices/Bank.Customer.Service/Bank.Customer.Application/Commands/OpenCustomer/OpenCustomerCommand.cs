using Bank.Customer.Domain.Shared;
using Mediator;

namespace Bank.Customer.Application.Commands.OpenCustomer;

public sealed record OpenCustomerCommand(
    string IdCard,
    string Name,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Address,
    DateTime IdCardDate,
    string IdCardPlace,
    string AccountNumber,
    int AccountType,
    string Password) : IRequest<Result>;
