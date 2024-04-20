using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Transaction.Contract;
using MassTransit;

namespace Bank.Balance.Infrastructure.Consumers;

internal class BuyPaymentDeniedConsumer(
    IBalanceRepository balanceRepository) : IConsumer<BuyPaymentDenied>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;

    public async Task Consume(ConsumeContext<BuyPaymentDenied> context)
    {
        var message = context.Message;

        await _balanceRepository.BuyPaymentDenied(
            message.AccountNumber,
            message.SecuritiesAccount,
            message.Amount);
    }
}
