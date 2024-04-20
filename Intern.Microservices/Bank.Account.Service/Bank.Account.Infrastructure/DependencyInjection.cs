using Bank.Account.Application.Abstraction.Data;
using Bank.Account.Application.Abstraction.Persistence;
using Bank.Account.Application.Abstraction.Settings;
using Bank.Account.Infrastructure.Consumers;
using Bank.Account.Infrastructure.Data;
using Bank.Account.Infrastructure.Persistence;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Account.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateCustomerAccountConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMQSetting = configuration
                    .GetSection(RabbitMQSetting.Section).Get<RabbitMQSetting>();

                cfg.Host(rabbitMQSetting!.Host, rabbitMQSetting.Port, rabbitMQSetting.VirtualHost, config =>
                {
                    config.Username(rabbitMQSetting.Username);
                    config.Password(rabbitMQSetting.Password);
                });

                cfg.ConfigureEndpoints(
                    context,
                    new KebabCaseEndpointNameFormatter(rabbitMQSetting.Prefix, false));

                cfg.UseMessageRetry(retryConfiguration =>
                {
                    retryConfiguration.Interval(
                        rabbitMQSetting.RetryCount,
                        TimeSpan.FromSeconds(rabbitMQSetting.Interval));
                });
            });
        });

        services.AddSingleton<IDataAccess, DataAccess>();

        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}
