namespace Bank.Transaction.Api.Responses;

public sealed record ResponseDataObject<T>(int Code, string Message, T? Data);
