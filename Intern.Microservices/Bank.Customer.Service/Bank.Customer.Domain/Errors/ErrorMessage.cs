namespace Bank.Customer.Domain.Errors;

public static class ErrorMessage
{
    public static Error NotFound { get; } = new(
        1, "No data was found", 404);
    public static Error Exception { get; } = new(
        2, "Error exeception", 500);
    public static Error BadRequest { get; } = new(
        3, "A validation problem occured", 400);

    public static Error IdCardNotBelongToAccount { get; } = new(
        10, "Id card does not belong to this account", 400);
    public static Error AccountNotHaveBalance { get; } = new(
        11, "Account does not have balance", 400);
    public static Error TakenAmountExceedBalance { get; } = new(
        12, "The amount taken exceeds the account balance", 400);
    public static Error HoldAmountExceedUsable { get; } = new(
        13, "The hold amount exceeds the usable balance", 400);
    public static Error UnholdAmountExceedHold { get; } = new(
        14, "The unhold amount exceeds the hold balance", 400);
    public static Error AccountNumberUsed { get; } = new(
        15, "Account number is used, please choose other", 400);
    public static Error IdCardUsed { get; } = new(
        16, "ID card is used, please check again", 400);
    public static Error InvalidCredential { get; } = new(
        17, "Wrong username and password", 400);

    public static Error Unauthorized { get; } = new(
        19, "Unauthorized", 401);
    public static Error TokenNotExpired { get; } = new(
        20, "Token has not expired", 400);
}
