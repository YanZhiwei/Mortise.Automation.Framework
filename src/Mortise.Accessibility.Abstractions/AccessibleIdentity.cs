using System.Drawing;

namespace Mortise.Accessibility.Abstractions;

public abstract class AccessibleIdentity
{
    public string ProcessName { get; protected set; } = "*";
    public AccessiblePriority Priority { get; protected set; } = AccessiblePriority.Normal;
    public abstract AccessibleComponent? FromPoint(Point location);
    public abstract AccessibleComponent? DtoAccessibleComponent(object element, Accessible? accessibility = null);
}