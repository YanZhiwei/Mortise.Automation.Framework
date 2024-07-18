using Mortise.BrowserAccessibility;
using Mortise.Platform;

namespace Mortise.ChromiumAccessibility.Configurations;

public sealed class ChromiumAccessibleOptions
{
    internal IList<IChromiumAccessibleOptionsExtension> AccessibleOptions { get; } =
        new List<IChromiumAccessibleOptionsExtension>();

    public IBrowserAccessibleContext AccessibleContext { get; set; }

    public ISystemInteraction SystemInteraction { get; set; }

    public void RegisterAccessible(IChromiumAccessibleOptionsExtension accessibleOptionsExtension)
    {
        if (accessibleOptionsExtension == null)
            throw new ArgumentNullException(nameof(accessibleOptionsExtension));

        AccessibleOptions.Add(accessibleOptionsExtension);
    }
}