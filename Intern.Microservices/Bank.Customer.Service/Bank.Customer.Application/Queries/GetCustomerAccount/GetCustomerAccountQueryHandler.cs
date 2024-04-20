using Bank.Customer.Application.Abstraction.Persistence;
using Bank.Customer.Domain.Errors;
using Bank.Customer.Domain.Shared;
using Bank.Customer.Domain.ValueObjects;
using Mediator;

namespace Bank.Customer.Application.Queries.GetCustomerAccount;

internal class GetCustomerAccountQueryHandler(
    ICustomerRepository customerRepository) : IRequestHandler<GetCustomerAccountQuery, Result<CustomerAccount>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async ValueTask<Result<CustomerAccount>> Handle(
        GetCustomerAccountQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _customerRepository
            .GetCustomerAccount(request.AccountNumber, request.IdCard);

        if (result is null)
        {
            return Result<CustomerAccount>.Failure(ErrorMessage.NotFound);
        }

        return Result<CustomerAccount>.Success(result);
    }
}
