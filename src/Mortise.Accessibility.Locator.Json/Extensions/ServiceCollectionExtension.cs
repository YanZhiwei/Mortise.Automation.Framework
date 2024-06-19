using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mortise.Accessibility.Locator.Abstractions;
using Tenon.Serialization.Json.Extensions;

namespace Mortise.Accessibility.Locator.Json.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddJsonLocatorStorage(this IServiceCollection services)
    {
        services.TryAddSingleton<IAccessibleLocatorStorage, JsonAccessibleLocatorStorage>();
        services.AddSystemTextJsonSerializer();
        return services;
    }
}