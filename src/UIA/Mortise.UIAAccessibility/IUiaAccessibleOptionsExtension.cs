using Microsoft.Extensions.DependencyInjection;

namespace Mortise.UiaAccessibility;

public interface IUiaAccessibleOptionsExtension
{
    void AddAccessible(IServiceCollection services);
}