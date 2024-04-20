namespace Bank.Balance.Api.Responses;

public sealed record ResponseDataObject<T>(int Code, string Message, T? Data);
