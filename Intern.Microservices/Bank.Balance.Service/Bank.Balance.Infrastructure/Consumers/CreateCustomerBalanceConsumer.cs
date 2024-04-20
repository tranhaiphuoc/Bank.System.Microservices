using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Customer.Contract.OpenCustomer;
using MassTransit;

namespace Bank.Balance.Infrastructure.Consumers;

internal class CreateCustomerBalanceConsumer(
    IBalanceRepository balanceRepository) : IConsumer<CreateCustomerBalance>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;

    public async Task Consume(ConsumeContext<CreateCustomerBalance> context)
    {
        var message = context.Message;

        await _balanceRepository.AddBalance(message.IdCard, message.AccountNumber);
    }
}
