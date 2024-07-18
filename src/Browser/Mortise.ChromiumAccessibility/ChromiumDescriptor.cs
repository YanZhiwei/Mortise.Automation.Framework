using Mortise.BrowserAccessibility;

namespace Mortise.ChromiumAccessibility;

public sealed class ChromiumDescriptor : BrowserDescriptor
{
    public ChromiumDescriptor()
    {
        SupportedProcessNames = ["chrome", "msedge"];
        IdentityString = string.Join(",", SupportedProcessNames);
    }

    public ChromiumBranch Branch { get; set; }
}