using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Transaction.Contract;
using MassTransit;

namespace Bank.Balance.Infrastructure.Consumers;

internal class DepositMoneyApprovalConsumer(
    IBalanceRepository balanceRepository) : IConsumer<DepositMoneyApproved>
{
    private readonly IBalanceRepository _balanceRepository = balanceRepository;

    public async Task Consume(ConsumeContext<DepositMoneyApproved> context)
    {
        var message = context.Message;

        await _balanceRepository.DepositMoneyApproved(message.AccountNumber, message.Amount);
    }
}
