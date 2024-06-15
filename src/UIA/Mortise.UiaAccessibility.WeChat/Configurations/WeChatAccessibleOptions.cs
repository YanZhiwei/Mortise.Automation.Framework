using Mortise.UiaAccessibility.Configurations;
using Mortise.UiaAccessibility.WeChat.Extensions;

namespace Mortise.UiaAccessibility.WeChat.Configurations;

public static class WeChatAccessibleOptions
{
    public static UiaAccessibleOptions AddWeChatAccessible(this UiaAccessibleOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));
        options.RegisterAccessible(new WeChatAccessibleOptionsExtension(options));
        return options;
    }
}