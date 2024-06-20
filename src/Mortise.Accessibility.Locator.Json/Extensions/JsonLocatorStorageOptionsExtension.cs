using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mortise.Accessibility.Locator.Abstractions;
using Mortise.Accessibility.Locator.Json.Configurations;

namespace Mortise.Accessibility.Locator.Json.Extensions;

internal sealed class JsonLocatorStorageOptionsExtension(JsonLocatorStorageOptions options)
    : IAccessibleLocatorOptionsExtension
{
    private readonly JsonLocatorStorageOptions _options = options ?? throw new ArgumentNullException(nameof(options));

    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(_options);
        services.TryAddSingleton<IAccessibleLocatorStorage, JsonAccessibleLocatorStorage>();
    }
}