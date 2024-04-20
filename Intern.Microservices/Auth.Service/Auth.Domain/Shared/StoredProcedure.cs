namespace Auth.Domain.Shared;

public static class StoredProcedure
{
    public static string GetUser { get; } = "getUser";
    public static string GetRefreshToken { get; } = "getRefreshToken";
    public static string AddUser { get; } = "addUser";
    public static string AddRefreshToken { get; } = "addRefreshToken";
    public static string RefreshAccessToken { get; } = "refreshAccessToken";
    public static string RevokeRefreshToken { get; } = "revokeRefreshToken";
}
