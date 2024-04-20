using Bank.Customer.Domain.Shared;
using Bank.Customer.Domain.ValueObjects;
using Mediator;

namespace Bank.Customer.Application.Queries.IsCustomerAccountValid;

public sealed record IsCustomerAccountValidQuery(
    string AccountNumber,
    string IdCard) : IRequest<Result>;
