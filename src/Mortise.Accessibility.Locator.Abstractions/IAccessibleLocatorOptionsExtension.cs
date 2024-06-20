using Microsoft.Extensions.DependencyInjection;

namespace Mortise.Accessibility.Locator.Abstractions;

public interface IAccessibleLocatorOptionsExtension
{
    void AddServices(IServiceCollection services);
}