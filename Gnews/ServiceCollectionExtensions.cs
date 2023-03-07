using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gnews;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGnewsClient(this IServiceCollection services)
    {
        services.AddOptions<GnewsClientOptions>();
        services.AddHttpClient<GnewsClient>();
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.Configure<GnewsClientOptions>(configuration.GetSection(nameof(GnewsClientOptions)));
        return services;
    }

    public static IServiceCollection AddGnewsClient(this IServiceCollection services, Action<GnewsClientOptions> setupAction)
    {
        services.AddOptions<GnewsClientOptions>().Configure(setupAction);
        services.AddHttpClient<GnewsClient>();
        return services;
    }
}