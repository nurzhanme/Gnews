using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gnews;

public static class ServiceCollectionExtensions
{
    public static void AddGnewsClient(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<GnewsClientOptions>(configuration);
       
    public static void AddGnewsClient(this IServiceCollection services, Action<GnewsClientOptions> configureClient) =>
        services.Configure(configureClient);
}