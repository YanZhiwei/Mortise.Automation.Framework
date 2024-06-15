using Mortise.Accessibility.Abstractions;

namespace Mortise.UiaAccessibility.WeChat;

public class WeChatAccessibleMetadata : IAccessibleMetadata
{
    public WeChatAccessibleMetadata()
    {
        SupportedProcessNames = ["WeChat", "WeChatApp"];
        IdentityString = string.Join(",", SupportedProcessNames);
    }

    public string[] SupportedProcessNames { get; }
    public string IdentityString { get; }
}