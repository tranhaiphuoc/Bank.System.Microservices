using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Transaction.Contract;
using MassTransit;

namespace Bank.Balance.Infrastructure.Consumers;

internal class BuyPaymentApprovalConsumer(
    IBalanceRepository balanceRepository) : IConsumer<BuyPaymentApproved>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;

    public async Task Consume(ConsumeContext<BuyPaymentApproved> context)
    {
        var message = context.Message;

        await _balanceRepository.BuyPaymentApproved(
            message.AccountNumber,
            message.SecuritiesAccount,
            message.Amount);
    }
}
