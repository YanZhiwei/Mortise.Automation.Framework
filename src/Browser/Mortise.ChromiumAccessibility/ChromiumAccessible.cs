using System.Collections.Concurrent;
using System.Drawing;
using Mortise.Accessibility.Abstractions;
using Mortise.BrowserAccessibility;
using Mortise.BrowserAccessibility.Options;
using Mortise.Platform;
using Tenon.Mapper.Abstractions;

namespace Mortise.ChromiumAccessibility;

[Serializable]
public class ChromiumAccessible : BrowserAccessible
{
    public readonly ChromiumAccessibleDetector NativeDetector;
    public readonly ISystemInteraction SystemInteraction;

    public ChromiumAccessible(ISystemInteraction systemInteraction, IObjectMapper mapper,
        IBrowserAccessibleContext context, IEnumerable<IChromiumAccessibleDetector> chromiumDetectors) : base(context)
    {
        Descriptor = new ChromiumDescriptor();
        SystemInteraction = systemInteraction ?? throw new ArgumentNullException(nameof(systemInteraction));
        Detector = new ChromiumAccessibleDetector(this, systemInteraction, mapper, chromiumDetectors, Descriptor);
        NativeDetector = (ChromiumAccessibleDetector)Detector;
        Engine = BrowserEngine.Chromium;
        Browsers = new ConcurrentDictionary<ChromiumBranch, List<IBrowserAccessibleAction>>();
    }

    public ConcurrentDictionary<ChromiumBranch, List<IBrowserAccessibleAction>> Browsers { get; protected set; }

    public virtual IEnumerable<IBrowserAccessibleAction> GetBrowsers()
    {
        return Browsers.SelectMany(c => c.Value).ToArray();
    }
    public virtual async Task<IBrowserAccessibleAction?> LaunchAsync(LaunchOptions options, int timeoutSeconds = 30)
    {
        return await Task.FromResult<IBrowserAccessibleAction>(null);
    }

    public override void Record(object component)
    {
        throw new NotImplementedException();
    }

    public override AccessibleComponent? FindComponent(Accessible accessible)
    {
        throw new NotImplementedException();
    }

    public override async Task<IPageAccessibleAction> AttachTo(Point location, string process)
    {
        return await Task.FromResult<IPageAccessibleAction>(null);
    }
}