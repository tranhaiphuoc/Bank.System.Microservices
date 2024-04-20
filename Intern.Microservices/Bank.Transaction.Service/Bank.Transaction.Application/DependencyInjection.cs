using Microsoft.Extensions.DependencyInjection;

namespace Bank.Transaction.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

        return services;
    }
}
