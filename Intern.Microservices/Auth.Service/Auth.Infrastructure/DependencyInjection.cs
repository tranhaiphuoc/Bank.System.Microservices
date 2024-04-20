using Auth.Application.Abstraction.Data;
using Auth.Application.Abstraction.Persistence;
using Auth.Application.Abstraction.Services;
using Auth.Application.Abstraction.Settings;
using Auth.Infrastructure.Consumers;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Services;
using Auth.Infrastructure.TypeHandler;
using Dapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure;

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
            x.AddConsumer<CreateCustomerUserConsumer>();

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
                .AddScoped<IAuthRepository, AuthRepository>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IHelper, Helper>()
                .AddSingleton<IJwtSettings, JwtSettings>()
                .AddSingleton<IJwtTokenService, JwtTokenService>();


        return services;
    }
}
