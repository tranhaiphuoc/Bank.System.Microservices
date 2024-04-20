namespace Bank.Transaction.Api.Requests;

public sealed record ApprovalRequest(
    string TransactionId,
    int Status,
    string ApprovedBy);
