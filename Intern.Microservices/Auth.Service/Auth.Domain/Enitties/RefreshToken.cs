namespace Auth.Domain.Enitties;

public class RefreshToken
{
    public required string Token { get; set; }

    public Guid JwtId { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime ExpiredAt { get; set; }

    public bool IsRevoked { get; set; }

    public required string UserId { get; set; }
}
