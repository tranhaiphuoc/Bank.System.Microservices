using Bank.Transaction.Application.Abstraction.Data;
using Bank.Transaction.Application.Abstraction.Persistence;
using Bank.Transaction.Application.Abstraction.Services;
using Bank.Transaction.Application.Abstraction.Settings;
using Bank.Transaction.Infrastructure.Consumers;
using Bank.Transaction.Infrastructure.Data;
using Bank.Transaction.Infrastructure.Persistence;
using Bank.Transaction.Infrastructure.Services;
using Bank.Transaction.Infrastructure.TypeHandlers;
using Dapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Transaction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
        SqlMapper.RemoveTypeMap(typeof(Guid));
        SqlMapper.RemoveTypeMap(typeof(Guid?));

        services.AddMassTransit(x =>
        {
            x.AddConsumer<BuyPaymentTransactionConsumer>();
            x.AddConsumer<SellPaymentTransactionConsumer>();
            x.AddConsumer<DepositMoneyTransactionConsumer>();

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

        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
