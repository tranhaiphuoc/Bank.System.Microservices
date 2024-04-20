using Bank.Transaction.Domain.Shared;
using Mediator;

namespace Bank.Transaction.Application.Commands.SellPaymentApproval;

public sealed record SellPaymentApprovalCommand(
    string TransactionId,
    int Status,
    string ApprovedBy) : IRequest<Result>;
