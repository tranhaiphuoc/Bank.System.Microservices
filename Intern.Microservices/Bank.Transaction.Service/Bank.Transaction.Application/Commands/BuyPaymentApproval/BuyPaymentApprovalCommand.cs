using Bank.Transaction.Domain.Shared;
using Mediator;

namespace Bank.Transaction.Application.Commands.BuyPaymentApproval;

public sealed record BuyPaymentApprovalCommand(
    string TransactionId,
    int Status,
    string ApprovedBy) : IRequest<Result>;
