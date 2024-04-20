using Auth.Application.Abstraction.Persistence;
using Bank.Customer.Contract.OpenCustomer;
using MassTransit;

namespace Auth.Infrastructure.Consumers;

internal class CreateCustomerUserConsumer(
    IAuthRepository authRepository) : IConsumer<CreateCustomerUser>
{
    private readonly IAuthRepository _authRepository = authRepository;

    public async Task Consume(ConsumeContext<CreateCustomerUser> context)
    {
        var message = context.Message;

        await _authRepository.AddUser(
            message.IdCard,
            message.PasswordHash,
            message.AccountNumber);
    }
}
