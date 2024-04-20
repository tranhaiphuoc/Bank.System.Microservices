using Bank.Balance.Contract.DepositMoney;
using Bank.Transaction.Application.Abstraction.Persistence;
using MassTransit;

namespace Bank.Transaction.Infrastructure.Consumers;

internal class DepositMoneyTransactionConsumer(
    ITransactionRepository transactionRepository) : IConsumer<DepositMoneyTransaction>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task Consume(ConsumeContext<DepositMoneyTransaction> context)
    {
        var message = context.Message;

        await _transactionRepository.DepositMoney(
            message.AccountNumber,
            message.Amount,
            message.Description);
    }
}
