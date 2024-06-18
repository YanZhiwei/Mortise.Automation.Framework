using Microsoft.Extensions.DependencyInjection;
using Mortise.Accessibility.Abstractions;
using Mortise.UiaAccessibility.Configurations;
using Mortise.UiaAccessibility.Converters;
using Tenon.Mapper.AutoMapper.Extensions;
using Tenon.Serialization.Json;
using Tenon.Serialization.Json.Extensions;

namespace Mortise.UiaAccessibility.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUiaAccessible(this IServiceCollection services,
        Action<UiaAccessibleOptions> setupAction)
    {
        if (setupAction == null)
            throw new ArgumentNullException(nameof(setupAction));

        var options = new UiaAccessibleOptions();
        setupAction(options);


        foreach (var serviceExtension in options.AccessibleOptions)
            serviceExtension.AddAccessible(services);

        services.AddSingleton<Accessible, UiaAccessible>();
        services.AddSingleton<AccessibleIdentity, UiaAccessibleIdentity>();
        services.AddAutoMapperSetup(typeof(AutoMapperProfile).Assembly);
        var jsonSerializerOptions = SystemTextJsonSerializer.DefaultOptions;
        jsonSerializerOptions.WriteIndented = true;
        jsonSerializerOptions.Converters.Add(new UiaAccessibleComponentConverter());
        services.AddSystemTextJsonSerializer(jsonSerializerOptions);
        return services;
    }
}