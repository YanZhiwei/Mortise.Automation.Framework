using Microsoft.Extensions.DependencyInjection;
using Mortise.UIAAccessibility;
using Mortise.UiaAccessibility.Configurations;

namespace Mortise.UiaAccessibility.WeChat.Extensions;

internal sealed class WeChatAccessibleOptionsExtension : IUiaAccessibleOptionsExtension
{
    private readonly UiaAccessibleOptions _options;

    public WeChatAccessibleOptionsExtension(UiaAccessibleOptions options)
    {
        _options = options;
    }

    public void AddAccessible(IServiceCollection services)
    {
        services.AddSingleton<IUiaAccessibleIdentity, WeChatAccessibleIdentity>();
    }
}