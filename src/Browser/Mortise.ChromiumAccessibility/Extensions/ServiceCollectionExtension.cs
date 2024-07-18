using Microsoft.Extensions.DependencyInjection;
using Mortise.Accessibility.Abstractions;
using Mortise.ChromiumAccessibility.Configurations;
using Tenon.Mapper.AutoMapper.Extensions;

namespace Mortise.ChromiumAccessibility.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddChromiumAccessible(this IServiceCollection services,
        Action<ChromiumAccessibleOptions> setupAction)
    {
        if (setupAction == null)
            throw new ArgumentNullException(nameof(setupAction));
        var options = new ChromiumAccessibleOptions();
        setupAction(options);
        foreach (var serviceExtension in options.AccessibleOptions)
            serviceExtension.AddAccessible(services);
        services.AddSingleton<Accessible, ChromiumAccessible>();
        services.AddSingleton<AccessibleDetector, ChromiumAccessibleDetector>();
        services.AddSingleton(options);
        if (options.AccessibleContext == null)
            throw new ArgumentNullException(nameof(options.AccessibleContext));
        services.AddSingleton(options.AccessibleContext);

        if (options.SystemInteraction == null)
            throw new ArgumentNullException(nameof(options.SystemInteraction));
        services.AddSingleton(options.SystemInteraction);

        services.AddAutoMapperSetup(typeof(AutoMapperProfile).Assembly);
        return services;
    }
}