using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Mortise.Accessibility.Locator.Json.Configurations;
using Tenon.Serialization.Json;
using Tenon.Serialization.Json.Extensions;

namespace Mortise.Accessibility.Locator.Json.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddJsonLocator(this IServiceCollection services,
        Action<JsonLocatorStorageOptions>? setupAction = null,
        JsonConverter[]? converters = null)
    {
        var jsonSerializerOptions = SystemTextJsonSerializer.DefaultOptions;
        jsonSerializerOptions.WriteIndented = true;
        if (converters?.Any() ?? false)
            foreach (var converter in converters)
                jsonSerializerOptions.Converters.Add(converter);
        if (setupAction != null)
        {
            var options = new JsonLocatorStorageOptions();
            setupAction(options);
            foreach (var serviceExtension in options.LocatorOptions)
                serviceExtension.AddServices(services);
        }

        services.AddSystemTextJsonSerializer(jsonSerializerOptions);
        return services;
    }
}