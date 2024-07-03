using Mortise.Accessibility.Abstractions;

namespace Mortise.UiaAccessibility.WeChat;

public class WeChatAccessibleDescriptor : IAccessibleDescriptor
{
    public WeChatAccessibleDescriptor()
    {
        SupportedProcessNames = ["WeChat", "WeChatApp"];
        IdentityString = string.Join(",", SupportedProcessNames);
    }

    public string[] SupportedProcessNames { get; }
    public string IdentityString { get; }
}