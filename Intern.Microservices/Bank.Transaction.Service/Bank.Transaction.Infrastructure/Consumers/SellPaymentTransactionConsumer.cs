using Bank.Balance.Contract.SellPayment;
using Bank.Transaction.Application.Abstraction.Persistence;
using MassTransit;

namespace Bank.Transaction.Infrastructure.Consumers;

internal class SellPaymentTransactionConsumer(
    ITransactionRepository transactionRepository) : IConsumer<SellPaymentTransaction>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task Consume(ConsumeContext<SellPaymentTransaction> context)
    {
        var message = context.Message;

        await _transactionRepository.SellPayment(
            message.AccountNumber,
            message.SecuritiesAccount,
            message.Amount,
            message.Description);
    }
}
