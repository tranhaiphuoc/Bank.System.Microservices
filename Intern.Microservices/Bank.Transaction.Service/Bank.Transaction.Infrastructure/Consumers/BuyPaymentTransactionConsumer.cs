using Bank.Balance.Contract.BuyPayment;
using Bank.Transaction.Application.Abstraction.Persistence;
using MassTransit;

namespace Bank.Transaction.Infrastructure.Consumers;

internal class BuyPaymentTransactionConsumer(
    ITransactionRepository transactionRepository) : IConsumer<BuyPaymentTransaction>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task Consume(ConsumeContext<BuyPaymentTransaction> context)
    {
        var message = context.Message;

        await _transactionRepository.BuyPayment(
            message.AccountNumber,
            message.SecuritiesAccount,
            message.Amount,
            message.Description);
    }
}
