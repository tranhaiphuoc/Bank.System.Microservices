using Bank.Customer.Application.Abstraction.Persistence;
using Bank.Customer.Application.Abstraction.Services;
using Bank.Customer.Contract.OpenCustomer;
using Bank.Customer.Domain.Shared;
using MassTransit;
using Mediator;

namespace Bank.Customer.Application.Commands.OpenCustomer;

internal class OpenCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IPublishEndpoint publishEndpoint,
    IHelper helper) : IRequestHandler<OpenCustomerCommand, Result>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IHelper _helper = helper;

    public async ValueTask<Result> Handle(
        OpenCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var passwordHash = await _helper.HashPassword(request.Password);

        var errorCode = await _customerRepository.OpenCustomer(
            request.IdCard,
            request.Name,
            request.DateOfBirth,
            request.PhoneNumber,
            request.Address,
            request.IdCardDate,
            request.IdCardPlace,
            request.AccountNumber,
            request.AccountType,
            passwordHash);

        var result = await _helper.GetError(errorCode.Code);

        if (result.IsSuccess)
        {
            Task[] messagingTasks =
            [
                _publishEndpoint.Publish<CreateCustomerUser>(
                    new(request.IdCard, passwordHash, request.AccountNumber),
                    cancellationToken),
                _publishEndpoint.Publish<CreateCustomerAccount>(
                    new(request.IdCard, request.AccountNumber, request.AccountType),
                    cancellationToken),
                _publishEndpoint.Publish<CreateCustomerBalance>(
                    new(request.IdCard, request.AccountNumber),
                    cancellationToken)
            ];

            await Task.WhenAll(messagingTasks);
        }

        return result;
    }
}
