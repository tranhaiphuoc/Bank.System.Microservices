using Bank.Customer.Application.Abstraction.Persistence;
using Bank.Customer.Application.Abstraction.Services;
using Bank.Customer.Domain.Shared;
using Mediator;

namespace Bank.Customer.Application.Queries.IsCustomerAccountValid;

internal class IsCustomerAccountValidQueryHandle(
    ICustomerRepository customerRepository,
    IHelper helper) : IRequestHandler<IsCustomerAccountValidQuery, Result>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IHelper _helper = helper;

    public async ValueTask<Result> Handle(
        IsCustomerAccountValidQuery request,
        CancellationToken cancellationToken)
    {
        var errorCode = await _customerRepository
            .IsCustomerAccountValid(request.AccountNumber, request.IdCard);

        return await _helper.GetError(errorCode.Code);
    }
}
