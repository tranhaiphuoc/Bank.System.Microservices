using Bank.Balance.Application.Abstraction.Data;
using Bank.Balance.Application.Abstraction.Persistence;
using Bank.Balance.Application.Abstraction.Services;
using Bank.Balance.Application.Abstraction.Settings;
using Bank.Balance.Infrastructure.Consumers;
using Bank.Balance.Infrastructure.Data;
using Bank.Balance.Infrastructure.Persistence;
using Bank.Customer.Infrastructure.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Balance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateCustomerBalanceConsumer>();
            x.AddConsumer<DepositMoneyApprovalConsumer>();
            x.AddConsumer<BuyPaymentApprovalConsumer>();
            x.AddConsumer<BuyPaymentDeniedConsumer>();
            x.AddConsumer<SellPaymentApprovalConsumer>();
            x.AddConsumer<SellPaymentDeniedConsumer>();


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

        services.AddScoped<IBalanceRepository, BalanceRepository>();

        return services;
    }
}
