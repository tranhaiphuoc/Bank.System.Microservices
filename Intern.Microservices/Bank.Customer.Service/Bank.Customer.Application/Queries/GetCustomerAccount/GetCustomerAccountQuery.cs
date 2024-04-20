using Bank.Customer.Domain.Shared;
using Bank.Customer.Domain.ValueObjects;
using Mediator;

namespace Bank.Customer.Application.Queries.GetCustomerAccount;

public sealed record GetCustomerAccountQuery(
    string AccountNumber,
    string IdCard) : IRequest<Result<CustomerAccount>>;
