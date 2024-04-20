using Bank.Customer.Application.Abstraction.Data;
using Bank.Customer.Application.Abstraction.Persistence;
using Bank.Customer.Application.Abstraction.Services;
using Bank.Customer.Application.Abstraction.Settings;
using Bank.Customer.Infrastructure.Data;
using Bank.Customer.Infrastructure.Persistence;
using Bank.Customer.Infrastructure.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Customer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
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

        services.AddSingleton<IDataAccess, DataAccess>()
                .AddSingleton<IHelper, Helper>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
