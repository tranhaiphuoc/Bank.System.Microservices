using Bank.Account.Application.Abstraction.Persistence;
using Bank.Customer.Contract.OpenCustomer;
using MassTransit;

namespace Bank.Account.Infrastructure.Consumers;

public class CreateCustomerAccountConsumer(
    IAccountRepository accountRepository) : IConsumer<CreateCustomerAccount>
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task Consume(ConsumeContext<CreateCustomerAccount> context)
    {
        var message = context.Message;

        await _accountRepository
            .CreateAccountAsync(message.IdCard, message.AccountNumber, message.TypeId);
    }
}
