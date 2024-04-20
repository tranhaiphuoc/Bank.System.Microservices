using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Transaction.Contract;
using MassTransit;

namespace Bank.Balance.Infrastructure.Consumers;

internal class SellPaymentDeniedConsumer(
    IBalanceRepository balanceRepository) : IConsumer<SellPaymentDenied>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;

    public async Task Consume(ConsumeContext<SellPaymentDenied> context)
    {
        var message = context.Message;

        await _balanceRepository.SellPaymentDenied(
            message.AccountNumber,
            message.SecuritiesAccount,
            message.Amount);
    }
}
