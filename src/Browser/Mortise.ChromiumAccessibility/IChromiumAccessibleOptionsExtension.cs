using Microsoft.Extensions.DependencyInjection;

namespace Mortise.ChromiumAccessibility;

public interface IChromiumAccessibleOptionsExtension
{
    void AddAccessible(IServiceCollection services);
}