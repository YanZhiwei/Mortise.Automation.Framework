using Mortise.Accessibility.Abstractions;

namespace Mortise.BrowserAccessibility;

public class BrowserDescriptor : IAccessibleDescriptor
{
    public string ExecutablePath { get; set; }
    public HashSet<string> Profiles { get; set; }
    public Version Version { get; set; }
    public BrowserEngine Engine { get; set; }
    public string[] SupportedProcessNames { get; set; }
    public string IdentityString { get; set; }
}