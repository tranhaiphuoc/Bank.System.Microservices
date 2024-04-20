using Auth.Application.Abstraction.Services;

namespace Auth.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime CreateUtcDateTime(long seconds)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(seconds)
            .ToUniversalTime();
    }
}
