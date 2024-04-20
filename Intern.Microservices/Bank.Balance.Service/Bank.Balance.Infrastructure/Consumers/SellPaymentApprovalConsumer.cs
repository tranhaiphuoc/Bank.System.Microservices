using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Transaction.Contract;
using MassTransit;

namespace Bank.Balance.Infrastructure.Consumers;

internal class SellPaymentApprovalConsumer(
    IBalanceRepository balanceRepository) : IConsumer<SellPaymentApproved>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;

    public async Task Consume(ConsumeContext<SellPaymentApproved> context)
    {
        var message = context.Message;

        await _balanceRepository.SellPaymentApproved(
            message.AccountNumber,
            message.SecuritiesAccount,
            message.Amount);
    }
}
