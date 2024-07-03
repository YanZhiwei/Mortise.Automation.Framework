using Microsoft.Extensions.DependencyInjection;
using Mortise.Accessibility.Abstractions;
using Mortise.UiaAccessibility.Configurations;
using Tenon.Mapper.AutoMapper.Extensions;

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
        services.AddSingleton<AccessibleDetector, UiaAccessibleDetector>();
        services.AddAutoMapperSetup(typeof(AutoMapperProfile).Assembly);
        return services;
    }
}