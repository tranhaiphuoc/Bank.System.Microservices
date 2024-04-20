namespace Auth.Api.Contracts.Responses;

public sealed record ResponseDataObject<T>(int Code, string Message, T? Data);
