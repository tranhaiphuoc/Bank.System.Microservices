namespace Bank.Customer.Application.Abstraction.Settings;

public class RabbitMQSetting
{
    public static readonly string Section = "RabbitMQ";

    public required string Host { get; set; }
    public ushort Port { get; set; }
    public required string VirtualHost { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Prefix { get; set; }
    public int RetryCount { get; set; }
    public double Interval { get; set; }
}
