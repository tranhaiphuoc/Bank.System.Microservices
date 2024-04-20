using Bank.Transaction.Domain.Shared;
using Mediator;

namespace Bank.Transaction.Application.Commands.DepositApproval;

public sealed record DepositApprovalCommand(
    string TransactionId,
    int Status,
    string ApprovedBy) : IRequest<Result>;
