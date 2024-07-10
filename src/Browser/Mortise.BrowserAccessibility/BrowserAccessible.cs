using System.Drawing;
using Mortise.Accessibility.Abstractions;

namespace Mortise.BrowserAccessibility;

public abstract class BrowserAccessible(IBrowserAccessibleContext context) : Accessible
{
    protected readonly IBrowserAccessibleContext Context = context ?? throw new ArgumentNullException(nameof(context));
    protected BrowserDescriptor Descriptor { get; set; }
    public BrowserEngine Engine { get; protected set; }
    public abstract Task<IPageAccessibleAction> AttachTo(Point location, string process);
}