namespace Auth.Application.Abstraction.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateTime CreateUtcDateTime(long seconds);
}
